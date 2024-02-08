using System;
using System.Collections.Generic;
using UnityEngine;
using ViewModel.Core;

namespace ViewModel.StateMachines
{
    public class GameStateMachine
    {
        private GameState CurrentGameState { get; set; }
        private readonly Dictionary<Type, GameState> _states = new();
        
        public void AddState(GameState state)
        {
            Debug.Log($"{state.GetType()} added");
            _states.Add(state.GetType(),state);
        }
        public void SetState<T>()
        {
            var type = typeof(T);
            if (CurrentGameState?.GetType() == type)
            {
                return;
            }
            if (!_states.TryGetValue(type, out var newState)) return;
            CurrentGameState?.Exit();
            CurrentGameState = newState;
            CurrentGameState.Enter();
        }
        public void Update()
        {
            CurrentGameState?.Update();
        }
    }
}