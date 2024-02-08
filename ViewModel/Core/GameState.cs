using ViewModel.StateMachines;

namespace ViewModel.Core
{
    public abstract class GameState
    {
        protected readonly GameStateMachine Gsm;
        protected GameState(GameStateMachine gsm)
        {
            Gsm = gsm;
        }
        public virtual void Enter(){}
        public virtual void Update(){}
        public virtual void Exit(){}
    }
}