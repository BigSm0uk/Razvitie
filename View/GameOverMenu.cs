using UnityEngine;
using UnityEngine.UI;
using ViewModel;

namespace View
{
    public class GameOverMenu : MonoBehaviour
    {
        [SerializeField] private Button catalogButton;
        [SerializeField] private Button commercialOfferButton;
        [SerializeField] private GameObject gameOverMenu;
        private int _level;
        private void Start()
        {
            SimpleEventBus.GameOverMenuActive.OnChanged += OnChanged;
        }
        private void Awake()
        {
            
        }

        private void OnChanged(bool active)
        {
            SimpleEventBus.ExitMenuActive.Value = false;
            gameOverMenu.SetActive(active);
        }
    }
}