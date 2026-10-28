using Assets.LastDefender.Scripts.Core.FSM.Base;
using Assets.LastDefender.Scripts.Core.FSM.Enemy;
using Assets.LastDefender.Scripts.Core.Interfaces;
using Assets.LastDefender.Scripts.Core.Models;
using Assets.LastDefender.Scripts.Runtime.CastleScripts;
using Assets.LastDefender.Scripts.Runtime.Common;
using Assets.LastDefender.Scripts.Runtime.Items.Base;
using Assets.LastDefender.Scripts.Runtime.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Assets.LastDefender.Scripts.Runtime.EnemyScripts.Base
{
    public class Enemy : MonoBehaviour, IDestructible
    {
        [Header("Movement")]
        [SerializeField] private Collider _collider;
        [field: SerializeField] public NavMeshAgent Agent { get; private set; }
        [field: SerializeField] public List<Transform> Waypoints { get; private set; } = new List<Transform>();

        [Header("Detection")]
        [field: SerializeField] public float DetectionRange { get; private set; }
        [field: SerializeField] public float AttackRange { get; private set; }
        [SerializeField] private LayerMask _detectionLayer;

        [Header("Drops")]
        [SerializeField] private Item _itemPrefab;
        [SerializeField] private List<DropedItem> _droppedItems;

        [field: SerializeField] public Animator Animator { get; private set; }
        private AttackBox _attackBox;

        public int CurrentWaypointIndex { get; set; } = 0;

        public EnemyInfo EnemyInfo { get; private set; }
        public event Action<Enemy> OnDie;

        #region States fields
        private StateMachine _stateMachine;

        public IdleState IdleState { get; private set; }
        public OffensiveState OffensiveState { get; private set; }
        public PersecutionState PersecutionState { get; private set; }
        public AttackState AttackState { get; private set; }
        public DieState DieState { get; private set; }
        #endregion
        public event Action<int> ChangedHealthEvent;
        [SerializeField] private  EnemyStatsRender _enemyStatsRender;

        
        public void Init(EnemyData data)
        {
            Waypoints.Add(PointManager.Instance.MainTarget);
            EnemyInfo = new EnemyInfo(data);

            _attackBox = GetComponentInChildren<AttackBox>();
            _attackBox.Init(EnemyInfo.Data.Damage);

            InitStates();
            _enemyStatsRender.Init(EnemyInfo.Data.Health);
            Agent.speed = EnemyInfo.Data.Speed;
        }
        private void OnEnable()
        {

            SubscribeEvents();
        }
        private void Update()
        {
            _stateMachine?.CurrentState.Update();
        }

        private void FixedUpdate()
        {
            _stateMachine?.CurrentState.FixedUpdate();
        }

        private InventoryItem DropItems()
        {
            int totalChance = 0;
            foreach (var item in _droppedItems)
                totalChance += item.Chance;

            int randomValue = UnityEngine.Random.Range(0, totalChance);
            int current = 0;

            foreach (var drop in _droppedItems)
            {
                current += drop.Chance;

                if (randomValue < current)
                {
                    int amount = UnityEngine.Random.Range(drop.MinAmount, drop.MaxAmount + 1);
                    return new InventoryItem(drop.ItemData, amount);
                }
            }

            return null;
        }

        public Transform CheckTarget(float range)
        {
            Collider[] _buffer = new Collider[10];
            int count = Physics.OverlapSphereNonAlloc(transform.position, range, _buffer, _detectionLayer);

            if (count == 0)
                return null;

            var hit = _buffer[0];

            Transform t = hit.transform;
            for (int i = 0; i < 3 && t != null; i++)
            {
                if (t.TryGetComponent<Castle>(out _))
                    return Waypoints[CurrentWaypointIndex];

                t = t.parent;
            }

            return hit.transform;
        }

        public void TakeDamage(int damage)
        {
            if (EnemyInfo.CurrentHealth <= 0) 
                return;

            EnemyInfo.CurrentHealth -= damage;
            ChangedHealthEvent?.Invoke((int)EnemyInfo.CurrentHealth);

            if (EnemyInfo.CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            var generatedItem = Instantiate(_itemPrefab, transform.position, Quaternion.identity);
            var items = DropItems();
            generatedItem.Init(items);
            _stateMachine.ChangeState(DieState);
        }

        public void Spawn()
        {
            gameObject.SetActive(true);
            EnemyInfo.CurrentHealth = EnemyInfo.Data.Health;
            _enemyStatsRender.Init(EnemyInfo.Data.Health);
            _stateMachine.ChangeState(OffensiveState);
        }

        private void InitStates()
        {
            _stateMachine = new StateMachine();

            IdleState = new IdleState(this, _stateMachine);
            OffensiveState = new OffensiveState(this, _stateMachine);
            PersecutionState = new PersecutionState(this, _stateMachine);
            AttackState = new AttackState(this, _stateMachine);
            DieState = new DieState(this, _stateMachine);

            _stateMachine.Init(OffensiveState);
        }

        public void ActivateAttackBox()
        {
            _attackBox.Collider.enabled = true;
        }

        public void DeactivateAttackBox()
        {
            _attackBox.Collider.enabled = false;
        }

        public void EndDieAnimation()
        {
            OnDie?.Invoke(this);
        }

        public void SubscribeEvents()
        {
            ChangedHealthEvent += _enemyStatsRender.ChangedStats;

        }
    }
}
