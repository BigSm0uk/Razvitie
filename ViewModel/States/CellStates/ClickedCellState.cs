using Model;
using Model.Controllers;
using Model.Core;
using UnityEngine;
using ViewModel.Core;
using ViewModel.StateMachines;

namespace ViewModel.States.CellStates
{
    public class ClickedCellState : CellState
    {
        private const int EpsAngle = 5;

        public ClickedCellState(CellStateMachine csm, Cell cell) : base(csm, cell)
        {
        }

        public override void Enter()
        {
#if UNITY_EDITOR
            Debug.Log($"Pointer click in {Cell.gameObject.name}");
#endif
            Cell.RotationDegreesAmount = (Cell.RotationDegreesAmount + 90f);
            SimpleEventBus.ExitMenuActive.OnChanged += OnExitMenu;
            if (Cell.CellType == CellType.None) return;

            CellController.RotateCellOf90Corner(Cell);
            Cell.OnRotatedCell(Cell);
            Cell.OnFinishCheck();
            Cell.OnFinish += Finish;
        }

        private void Finish()
        {
            Csm.SetState<UnClickableCellState>();
        }

        public override void Update()
        {
            Cell.transform.rotation =
                Quaternion.AngleAxis(Cell.transform.rotation.eulerAngles.z + (Time.deltaTime * Cell.rotationVelocity),
                    Vector3.forward);
            if ((int)Cell.transform.localEulerAngles.z < (int)Cell.RotationDegreesAmount - EpsAngle)
            {
                return;
            }

            Cell.RotationDegreesAmount %= 360;
            Cell.transform.localEulerAngles = new Vector3(0.0f, 0.0f, Cell.RotationDegreesAmount);
            if (Cell.isFinished)
            {
                Finish();
                return;
            }

            Csm.SetState<DefaultCellState>();
        }

        private void OnExitMenu(bool exitMenuActive)
        {
            if (exitMenuActive)
            {
                Csm.SetState<UnClickableCellState>();
            }
        }

        public override void Exit()
        {
            Cell.OnFinish -= Finish;
        }
    }
}