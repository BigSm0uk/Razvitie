using ViewModel.Core;

namespace ViewModel
{
    public static class SimpleEventBus
    {
        public static  ReactiveProperty<bool> ExitMenuActive = new() { Value = false };
        public static ReactiveProperty<bool> GameOverMenuActive = new() { Value = false };
        public static  ReactiveProperty<bool> FinishMenuActive = new() { Value = false };
        public static  ReactiveProperty<bool> WinAnimationActive = new() { Value = false };
    }
}