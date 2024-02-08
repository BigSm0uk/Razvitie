using Model;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewModel.Core;
using ViewModel.StateMachines;

namespace ViewModel.States.CellStates
{
    public class SelectedCellState : CellState
    {
        public SelectedCellState(CellStateMachine csm, Cell cell) : base(csm, cell)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR

            Debug.Log($"Pointer enter in {Cell.gameObject.name}");
#endif
            Cell.PointerChanged += UnSelect;
            Cell.PointerClick += Click;
            Cell.OnFinish += Finish;
        }

        private void Finish()
        {
            Csm.SetState<UnClickableCellState>();
        }

        public override void Update()
        {
        }

        public override void Exit()
        {
#if UNITY_EDITOR
            Debug.Log($"Pointer exit in {Cell.gameObject.name}");
#endif
            Cell.PointerChanged -= UnSelect;
            Cell.PointerClick -= Click;
            Cell.OnFinish -= Finish;
        }

        private void UnSelect(bool selected, Cell sender)
        {
            Csm.SetState<DefaultCellState>();
        }

        private void Click(PointerEventData pointerEventData)
        {
            Csm.SetState<ClickedCellState>();
        }
    }
}