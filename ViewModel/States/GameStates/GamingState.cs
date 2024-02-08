using ViewModel.Core;
using ViewModel.StateMachines;

namespace ViewModel.States.GameStates
{
    public class GamingState : GameState
    {
        private readonly ReactiveProperty<bool> _timerOn;
        public GamingState(GameStateMachine gsm,
            ReactiveProperty<bool> timerOn) : base(gsm)
        {
            _timerOn = timerOn;

        }
        public override void Enter()
        {
            _timerOn.OnChanged += GameOver;
            SimpleEventBus.ExitMenuActive.OnChanged += GameExitMenu;
            SimpleEventBus.WinAnimationActive.OnChanged += GameFinished;
            SimpleEventBus.GameOverMenuActive.OnChanged += GameOver;

        }

        public override void Update()
        {
            
        }

        private void GameExitMenu(bool isGameContinue)
        {
            if (!isGameContinue)
            {
                Gsm.SetState<ExitMenuState>();
            }
        }

        private void GameFinished(bool isGameFinished)
        {
            if (isGameFinished)
            {
                Gsm.SetState<FinishLevelState>();
            }

        }

        private void GameOver(bool isGameOver)
        {
            if (isGameOver)
            {
                Gsm.SetState<GameOverMenuState>();
            }
        }

        public override void Exit()
        {
            _timerOn.OnChanged -= GameOver;
            SimpleEventBus.ExitMenuActive.OnChanged -= GameExitMenu;
            SimpleEventBus.WinAnimationActive.OnChanged -= GameFinished;
            SimpleEventBus.GameOverMenuActive.OnChanged -= GameOver;
        }
    }
}