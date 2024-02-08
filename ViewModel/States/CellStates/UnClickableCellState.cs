using Model;
using UnityEngine;
using ViewModel.Core;
using ViewModel.StateMachines;

namespace ViewModel.States.CellStates
{
    public class UnClickableCellState : CellState
    {
        public override void Enter()
        {
            SimpleEventBus.ExitMenuActive.OnChanged += OnExitMenu;
        }

        public override void Update()
        {
        }

        private void OnExitMenu(bool exitMenuActive)
        {
            if(!exitMenuActive && !SimpleEventBus.GameOverMenuActive.Value)
            {
                Csm.SetState<DefaultCellState>();
            }
        }

        

        public override void Exit()
        {
        }

        public UnClickableCellState(CellStateMachine csm, Cell cell) : base(csm, cell)
        {
        }
    }
}