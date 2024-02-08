using System;
using System.Collections.Generic;
using UnityEngine;
using ViewModel.Core;

namespace ViewModel.StateMachines
{
    public class CellStateMachine
    {
        private CellState CurrentCellState { get; set; }
        private readonly Dictionary<Type, CellState> _states = new();
        
        public void AddState(CellState state)
        {
            //Debug.Log($"{state.GetType()} added");
            _states.Add(state.GetType(),state);
        }
        public void SetState<T>()
        {
            var type = typeof(T);
            if (CurrentCellState?.GetType() == type)
            {
                return;
            }
            if (!_states.TryGetValue(type, out var newState)) return;
            CurrentCellState?.Exit();
            CurrentCellState = newState;
            CurrentCellState.Enter();
        }
        public void Update()
        {
            CurrentCellState?.Update();
        }
    }
}