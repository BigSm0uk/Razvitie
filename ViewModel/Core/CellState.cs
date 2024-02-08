using Model;
using ViewModel.StateMachines;

namespace ViewModel.Core
{
    public abstract  class CellState
    {
        protected readonly CellStateMachine Csm;
        protected readonly Cell Cell;

        protected CellState(CellStateMachine csm, Cell cell)
        {
            Csm = csm;
            Cell = cell;
        }
        public virtual void Enter(){}
        public virtual void Update(){}
        public virtual void Exit(){}
    }
}