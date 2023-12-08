using System;
using Enums;
using Signal;
using UnityEngine;
using Zenject;

namespace InputSystem
{
    public class InputHandler : ITickable, IDisposable
    {
        private readonly InputSignals _inputSignals;
        
        private readonly ButtonSignals _buttonSignals;
        
        private readonly int _layerMask = 1 << LayerMask.NameToLayer("Ground");
        
        private bool _isEnableInput;
        private bool _isCardSelected;
        
        public InputHandler(
            InputSignals inputSignals,
            ButtonSignals buttonSignals)
        {
            _inputSignals = inputSignals;
            _buttonSignals = buttonSignals;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _inputSignals.OnEnableInput += OnEnableInput;
            _buttonSignals.OnIsSelectedCard += OnIsSelectedCard;
        }
        
        private void OnEnableInput(bool value)
        {
            _isEnableInput = value;
        }
        
        private void OnIsSelectedCard(bool value)
        {
            _isCardSelected = value;
        }

        public void Tick()
        {
            if (!_isEnableInput)
            {
                return;
            }
            
            if (!_isCardSelected)
            {
                return;
            }
            
            if (!Physics.Raycast(Util.Utils.Ray, out RaycastHit hit, Mathf.Infinity,_layerMask))
            {
                return;
            }
            
            if (Input.GetMouseButtonDown(0))
            {
                _inputSignals.OnClickMouseButtonDown?.Invoke();
            }

            if (!Input.GetMouseButtonDown(1)) return;
            
            _buttonSignals.OnSetDeActiveAllCardPreview?.Invoke();
            _buttonSignals.OnIsSelectedCard?.Invoke(false);
        }
        
        private void UnsubscribeEvents()
        {
            _inputSignals.OnEnableInput -= OnEnableInput;
            _buttonSignals.OnIsSelectedCard -= OnIsSelectedCard;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}