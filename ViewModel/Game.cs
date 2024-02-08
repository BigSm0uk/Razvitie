using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Controllers;
using Model.Core;
using Model.Factories;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using ViewModel.Core;
using ViewModel.StateMachines;
using ViewModel.States;
using ViewModel.States.GameStates;

namespace ViewModel
{
    public class Game : MonoBehaviour
    {
        private GameStateMachine _gsm;


        [Header("InitData")] [SerializeField] [Range(1, 3)]
        private int gameLevel = 1;

        [SerializeField] private GameObject timer;
        [SerializeField] private TMP_Text timerUi;
        [SerializeField] private float timeLeft = 15;
        private ReactiveProperty<bool> _timerOn;

        #region GameBoardGrid

        [FormerlySerializedAs("_size")] [Header("GameBoardGrid")] [SerializeField]
        private Vector2 size;

        [SerializeField] private float spacing;
        [Space] [SerializeField] private Cell prefab;
        [Space] [SerializeField] private Transform root;
        [Space] [SerializeField] public List<Cell> cells;
#if UNITY_EDITOR
        private void OnValidate()
        {
            size = new Vector2((int)Mathf.Clamp(size.x, 0, Mathf.Infinity),
                (int)Mathf.Clamp(size.y, 0, Mathf.Infinity));
            spacing = Mathf.Clamp(spacing, 0, Mathf.Infinity);
            root ??= transform;
        }

        [ContextMenu("Create")]
        public void Create()
        {
            Clear();
            cells = GameBoardFactory.Create(prefab, size, spacing, root, gameLevel);
            foreach (var v in cells.Where(v => v.CellType != CellType.None))
            {
                CheckCellFriend(v);
            }
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            for (var i = 0; i < cells.Count;)
            {
                DestroyImmediate(cells[i].GameObject);
                cells.RemoveAt(i);
            }
        }
#endif
        #endregion



        private void Awake()
        {
            _timerOn = new ReactiveProperty<bool> { Value = true };

            timerUi = timer.GetComponent<TMP_Text>();
            gameLevel = PlayerPrefs.GetInt("GameLevel");

            _gsm = new GameStateMachine();
            _gsm.AddState(new InitLevelState(_gsm));
            _gsm.AddState(new GamingState(_gsm, _timerOn));
            _gsm.AddState(new ExitMenuState(_gsm, _timerOn));
            _gsm.AddState(new GameOverMenuState(_gsm));
            _gsm.AddState(new FinishLevelState(_gsm));

            _gsm.SetState<InitLevelState>();

            foreach (var t in cells)
            {
                t.RotatedCell += CheckCellFriend;
                t.FinishCheck += CheckFinish;
            }
        }

        private void Update()
        {
            _gsm.Update();
            CheckTimer();
        }

        private void CheckTimer()
        {
            if (!_timerOn.Value) return;
            if (timeLeft > 0)
            {
                UpdateTimeText();
                timeLeft -= Time.deltaTime;
            }
            else
            {
                _timerOn.Value = false;
                if (!SimpleEventBus.WinAnimationActive.Value)
                    SimpleEventBus.GameOverMenuActive.Value = true;
            }
        }

        private void UpdateTimeText()
        {
            if (timeLeft < 0)
                timeLeft = 0;

            float minutes = Mathf.FloorToInt(timeLeft / 60);
            float seconds = Mathf.FloorToInt(timeLeft % 60);
            timerUi.text = $"{minutes:00} : {seconds:00}";
        }

        private void CheckCellFriend(Cell sender)
        {
            CellController.SetCellFriend(cells, sender);
        }

        private void CheckFinish()
        {
            if (
#if UNITY_EDITOR
                true ||
#endif
                CellController.CheckForFinish(cells))
            {
                SimpleEventBus.WinAnimationActive.Value = true;
                _timerOn.Value = false;
                foreach (var cell in cells)
                {
                    cell.OnOnFinish();
                }
            }
        }
    }
}