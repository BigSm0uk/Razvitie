using UnityEngine;
using ViewModel.Core;
using ViewModel.StateMachines;

namespace ViewModel.States.GameStates
{
    //TODO: Отрисовать окно выйгрыша в уровне
    //TODO: Взять из PlayerPrefs данные о текущем уровне и скидке и изменить их в зависимости от номера пройденного уровня
    //TODO: Если пользователь согласен с текущей скидкой - дернуть JS ручку, если нет, загрузить новый уровень
    public class FinishLevelState : GameState
    {
        private const int AnimationTotalTime = 3;
        private float _animationTimer = 0;
        public FinishLevelState(GameStateMachine gsm) : base(gsm)
        {
        }
     
        public override void Enter()
        {
            
        }

        public override void Update()
        {
            if ((int)_animationTimer < AnimationTotalTime)
            {
                _animationTimer += Time.deltaTime;
            }
            else
            {
                SimpleEventBus.WinAnimationActive.Value = false;
                SimpleEventBus.FinishMenuActive.Value = true;
                
                SimpleEventBus.ExitMenuActive = new() { Value = false };
                SimpleEventBus.GameOverMenuActive = new() { Value = false };
                SimpleEventBus.FinishMenuActive = new() { Value = false };
                SimpleEventBus.WinAnimationActive = new() { Value = false };
            }
        }

        public override void Exit()
        {
        }
    }
}