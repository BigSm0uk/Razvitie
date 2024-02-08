using Model;
using UnityEngine.EventSystems;
using ViewModel.Core;
using ViewModel.StateMachines;

namespace ViewModel.States.CellStates
{
    public class DefaultCellState : CellState

    {
        public DefaultCellState(CellStateMachine csm, Cell cell) : base(csm, cell)
        {
        }

        public override void Enter()
        {
            Cell.PointerChanged += Select;
            Cell.PointerClick += Click;
            Cell.OnFinish += Finish;
            SimpleEventBus.ExitMenuActive.OnChanged += ExitOrGameOverMenu;
            SimpleEventBus.GameOverMenuActive.OnChanged += ExitOrGameOverMenu;
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
            Cell.PointerChanged -= Select;
            Cell.PointerClick -= Click;
            Cell.OnFinish -= Finish;
            SimpleEventBus.ExitMenuActive.OnChanged -= ExitOrGameOverMenu;
            SimpleEventBus.GameOverMenuActive.OnChanged -= ExitOrGameOverMenu;
        }

        private void Select(bool selected, Cell sender)
        {
            Csm.SetState<SelectedCellState>();
        }

        private void Click(PointerEventData pointerEventData)
        {
            Csm.SetState<ClickedCellState>();
        }

        private void Finish()
        {
            Csm.SetState<UnClickableCellState>();
        }

        private void ExitOrGameOverMenu(bool menuActive)
        {
            if (menuActive)
            {
                Csm.SetState<UnClickableCellState>();
            }
        }
    }
}