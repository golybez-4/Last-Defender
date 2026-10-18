using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.FSM.Base;
using UnityEngine;
using EnemyScripts = Assets.LastDefender.Scripts.Runtime.EnemyScripts.Base;

namespace Assets.LastDefender.Scripts.Core.FSM.Enemy
{
    public class AttackState : BaseState
    {
        private Transform _target;
        private bool _isAttackFinished;
        public AttackState(EnemyScripts.Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            _isAttackFinished = false;
            _enemy.Agent.isStopped = true;
            _enemy.Animator.Play(AnimationsState.Attack);
        }

        public override void Update()
        {
            var info = _enemy.Animator.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 1f && info.IsName(AnimationsState.Attack))
            {
                _isAttackFinished = true;
            }
        }

        public override void FixedUpdate()
        {
            if (_isAttackFinished)
            {
                _target = _enemy.CheckTarget(_enemy.AttackRange);
                if (_target == null)
                    _stateMachine.ChangeState(_enemy.PersecutionState);
            }
        }

        public override void Exit()
        {
            _enemy.Agent.isStopped = false;
        }
    }
}
