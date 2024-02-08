using System;
using UnityEngine;
using UnityEngine.UIElements;
using ViewModel.Core;

namespace ViewModel.Inputs
{
    public class PlayerInputs : MonoBehaviour
    {
        public readonly ReactiveProperty<bool> IsPause = new();
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("Pause Pressed");
                IsPause.Value = true;
            }
        }
        
    }
}