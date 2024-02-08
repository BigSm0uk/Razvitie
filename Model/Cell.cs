using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Model.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewModel.StateMachines;
using ViewModel.States.CellStates;

namespace Model
{
    public class Cell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Data")] [SerializeField] private Vector2Int position;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private CellType pipeType;
        [SerializeField] private SpecialCellType specialPipeType;
        [SerializeField] public float rotationVelocity = 180;
        [SerializeField] public bool isFinished;
        public SpecialCellType SpecialCellType => specialPipeType;
        public CellType CellType => pipeType;
        public int PositionX => position.x;
        public int PositionY => position.y;
        public List<int> CellMatrix => cellMatrix;
        public int Id => int.Parse($"{PositionX.ToString()}{PositionY.ToString()}");

        private CellStateMachine _csm;

        public delegate void PointerHandler(PointerEventData eventData);

        public delegate void PointerStateHandler(bool pointerEnter, Cell sender);

        public delegate void RotatedCellHandler(Cell sender);

        public delegate void FinishCheckHandler();

        public delegate void FinishHandler();

        public event RotatedCellHandler RotatedCell;
        public event FinishCheckHandler FinishCheck;
        public event PointerStateHandler PointerChanged;
        public event PointerHandler PointerClick;
        public event FinishHandler OnFinish;

       // [SerializeField] public ReactiveProperty<bool> finishStatus;

        [SerializeField] public float RotationDegreesAmount = 0f;
        private bool PointerEnter { get; set; }

        public GameObject GameObject => _gameObject;

        [Header("Components")] [SerializeField]
        private GameObject _gameObject;

        [SerializeField] [CanBeNull] public Cell leftCell;
        [SerializeField] [CanBeNull] public Cell rightCell;
        [SerializeField] [CanBeNull] public Cell upCell;
        [SerializeField] [CanBeNull] public Cell downCell;
        [SerializeField] private List<int> cellMatrix;


        public void Initialize(Vector2Int initPosition, CellType cellType, SpecialCellType specialCellType,
            List<int> initMatrix)
        {
            position = initPosition;
            pipeType = cellType;
            specialPipeType = specialCellType;
            cellMatrix = initMatrix;
            gameObject.name = $"Position:X: {position.x.ToString()}, Y: {position.y.ToString()}";
        }

        private void Awake()
        {
            rb ??= gameObject.GetComponent<Rigidbody2D>();

            _csm = new CellStateMachine();

            _csm.AddState(new DefaultCellState(_csm, this));
            _csm.AddState(new ClickedCellState(_csm, this));
            _csm.AddState(new SelectedCellState(_csm, this));
            _csm.AddState(new UnClickableCellState(_csm, this));

            _csm.SetState<DefaultCellState>();
        }

        private void Update()
        {
            _csm.Update();
        }

        private void OnValidate()
        {
            _gameObject ??= gameObject;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PointerClick?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEnter = true;
            PointerChanged?.Invoke(PointerEnter, this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerEnter = false;
            PointerChanged?.Invoke(PointerEnter, this);
        }

        public void OnRotatedCell(Cell sender)
        {
            RotatedCell?.Invoke(sender);
        }

        public void OnFinishCheck()
        {
            FinishCheck?.Invoke();
        }

        public void OnOnFinish()
        {
            OnFinish?.Invoke();
            isFinished = true;
        }
    }
}