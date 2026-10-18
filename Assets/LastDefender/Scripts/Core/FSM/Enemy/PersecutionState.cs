using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.FSM.Base;
using UnityEngine;

namespace Assets.LastDefender.Scripts.Core.FSM.Enemy
{
    public class PersecutionState : BaseState
    {
        private Transform _target;
        public PersecutionState(Runtime.EnemyScripts.Base.Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            _enemy.Agent.speed = _enemy.EnemyInfo.Data.RunSpeed;
            _enemy.Animator.Play(AnimationsState.Run);
            _target = _enemy.CheckTarget(_enemy.DetectionRange);
        }

        public override void FixedUpdate()
        {
            _target = _enemy.CheckTarget(_enemy.AttackRange);
            if (_target != null)
            {
                _stateMachine.ChangeState(_enemy.AttackState);
                return;
            }

            _target = _enemy.CheckTarget(_enemy.DetectionRange);
            if (_target == null)
            {
                _stateMachine.ChangeState(_enemy.OffensiveState);
                return;
            }
            _enemy.Agent.SetDestination(_target.position);
        }
    }
}
