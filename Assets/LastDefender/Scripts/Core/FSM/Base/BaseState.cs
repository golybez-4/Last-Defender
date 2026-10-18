using EnemyScripts = Assets.LastDefender.Scripts.Runtime.EnemyScripts.Base;

namespace Assets.LastDefender.Scripts.Core.FSM.Base
{
    public abstract class BaseState
    {
        protected readonly EnemyScripts.Enemy _enemy;
        protected readonly StateMachine _stateMachine;

        protected BaseState(EnemyScripts.Enemy enemy, StateMachine stateMachine)
        {
            _enemy = enemy;
            _stateMachine = stateMachine;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
    }
}
