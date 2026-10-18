using Assets.LastDefender.Scripts.Core.Common.Enums;
using Assets.LastDefender.Scripts.Core.FSM.Base;
using EnemyScripts = Assets.LastDefender.Scripts.Runtime.EnemyScripts.Base;

namespace Assets.LastDefender.Scripts.Core.FSM.Enemy
{
    public class DieState : BaseState
    {
        public DieState(EnemyScripts.Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
        {
        }

        public override void Enter()
        {
            _enemy.Animator.Play(AnimationsState.Die);
            _enemy.Agent.isStopped = true;
        }
    }
}
