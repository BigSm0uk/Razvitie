using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ViewModel;

namespace View
{
    public class FinishMenu : MonoBehaviour
    {
        [SerializeField] private Button nextLevelButton;
        [SerializeField] private Button takeDiscountButton;
        [SerializeField] private GameObject finishMenu;
        [SerializeField] private GameObject discountMenu;
        [SerializeField] private GameObject takeDiscountMenu;
        [SerializeField] private GameObject dogAnimation;
        [SerializeField] private GameObject dogPrefab;
        private int _level;

        private void Start()
        {
            SimpleEventBus.FinishMenuActive.OnChanged += OnChanged;
            SimpleEventBus.WinAnimationActive.OnChanged += OnActiveDogAnimation;
            SimpleEventBus.WinAnimationActive.OnChanged += OnDogPrefabActive;
        }

        private void Awake()
        {
            _level = PlayerPrefs.GetInt("GameLevel");
        }

        private void OnActiveDogAnimation(bool active)
        {
            dogAnimation.SetActive(active);
        }

        private void OnDogPrefabActive(bool active)
        {
            dogPrefab.SetActive(!active);
        }

        public void ShowDiscountMenu()
        {
            finishMenu.SetActive(false);
            discountMenu.SetActive(true);
        }

        public void ShowTakeNumberMenu()
        {
            discountMenu.SetActive(false);
            takeDiscountMenu.SetActive(true);
        }

        public void NextLevelClick()
        {
            SceneManager.LoadSceneAsync(_level);
        }

        private void OnChanged(bool active)
        {
            finishMenu.SetActive(active);
            _level += 1;
            PlayerPrefs.SetInt("GameLevel", _level);

        }
    }
}