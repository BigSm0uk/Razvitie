using Model.Core;
using UnityEngine;
using ViewModel.Core;
using ViewModel.StateMachines;
using ViewModel.States.GameStates;

namespace ViewModel.States
{
    public class InitLevelState : GameState
    {

        public InitLevelState(GameStateMachine gsm) : base(gsm)
        {
        }

        private static void InitGameLevel()
        {
            
        }
        public override void Enter()
        {
            InitGameLevel();
            Gsm.SetState<GamingState>();
        }

        public override void Update()
        { 
            
        }

        public override void Exit()
        {
            
        }
    }
}