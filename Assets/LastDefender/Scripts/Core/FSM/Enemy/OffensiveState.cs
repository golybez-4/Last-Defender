using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.FSM.Base;
using UnityEngine;
using EnemyScripts = Assets.LastDefender.Scripts.Runtime.EnemyScripts.Base;

namespace Assets.LastDefender.Scripts.Core.FSM.Enemy
{
    public class OffensiveState : BaseState
    {
        private Transform _target;
        public OffensiveState(EnemyScripts.Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            _enemy.Agent.speed = _enemy.EnemyInfo.Data.Speed;
            _enemy.Animator.Play(AnimationsState.Walk);
            Debug.Log("Entered Offensive State");
            _enemy.Agent.SetDestination(_enemy.Waypoints[_enemy.CurrentWaypointIndex].position);
        }

        public override void Update()
        {
            Offencive();
        }

        public override void FixedUpdate()
        {
            _target = _enemy.CheckTarget(_enemy.DetectionRange);

            if (_target != null)
                _stateMachine.ChangeState(_enemy.PersecutionState);
        }

        private void Offencive()
        {
            if (!_enemy.Agent.pathPending && _enemy.Agent.remainingDistance < 0.5f)
            {
                _enemy.CurrentWaypointIndex = (_enemy.CurrentWaypointIndex + 1) % _enemy.Waypoints.Count;
            }
        }
    }
}
