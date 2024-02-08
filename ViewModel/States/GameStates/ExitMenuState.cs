using ViewModel.Core;
using ViewModel.StateMachines;

namespace ViewModel.States.GameStates
{
    public class ExitMenuState : GameState
    {
        private readonly ReactiveProperty<bool> _timerOn;
        
        public ExitMenuState(GameStateMachine gsm,
            ReactiveProperty<bool> timerOn) : base(gsm)
        {
            _timerOn = timerOn;

        }

        public override void Enter()
        {
            _timerOn.OnChanged += TimerDone;
            SimpleEventBus.GameOverMenuActive.OnChanged += GameOver;
            SimpleEventBus.ExitMenuActive.OnChanged += GoToTheGameButtonPressed;
            

        }
        public override void Update(){}
        private void GameOver(bool isGameOver)
        {
            if (isGameOver)
            {
                Gsm.SetState<GameOverMenuState>();
            }
        }

        private void TimerDone(bool isTimerActive)
        {
            if (!isTimerActive)
            {
                Gsm.SetState<GameOverMenuState>();
            }
        }

        private void GoToTheGameButtonPressed(bool exitMenuActive)
        {
            if (exitMenuActive)
            {
                Gsm.SetState<GameOverMenuState>();
            }
        }
        
        
        public override void Exit()
        {
            _timerOn.OnChanged -= GameOver;
            SimpleEventBus.GameOverMenuActive.OnChanged -= GameOver;
            SimpleEventBus.ExitMenuActive.OnChanged -= GoToTheGameButtonPressed;
        }
        
    }
}