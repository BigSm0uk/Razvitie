using Model;
using ViewModel.Core;
using ViewModel.StateMachines;

namespace ViewModel.States.CellStates
{
    public class InitCellState : CellState
    {
        public InitCellState(CellStateMachine csm, Cell cell) : base(csm, cell)
        
        {
        }
        public override void Enter(){}
        public override void Update(){}
        public override void Exit(){}
    }
}