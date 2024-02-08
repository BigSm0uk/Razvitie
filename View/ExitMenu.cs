using UnityEngine;
using UnityEngine.SceneManagement;
using ViewModel;

namespace View
{
    public class ExitMenu : MonoBehaviour
    {
        [SerializeField] private GameObject exitMenu;

        private void Start()
        {
            SimpleEventBus.ExitMenuActive.OnChanged += OnChanged;
        }

        public void StayOrExitButtonClick()
        {
            SimpleEventBus.ExitMenuActive.Value = false;
        }

        public void GameOverClick()
        { 
            SceneManager.LoadScene(0);
        }

        public void SetActiveExitMenu()
        {
            SimpleEventBus.ExitMenuActive.Value = true;
        }

        private void OnChanged(bool active)
        {
            exitMenu.SetActive(active);
        }
    }
}