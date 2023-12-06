using System;
using Enums;
using Signal;
using UISystem.CardButtonSystem.View;

namespace UISystem.CardButtonSystem.State
{
    public class CardButtonStateManager : IDisposable
    {
        private CardButtonBaseState _currentState;
        
        private readonly CardButtonView _cardButtonView;
        
        private readonly CardButtonSelectionState _cardButtonSelectionState;
        
        private readonly CardButtonPlayState _cardButtonPlayState;
        
        private readonly ButtonSignals _buttonSignals;
        
        private CardButtonStateManager(
            CardButtonView cardButtonView,
            CardButtonSelectionState cardButtonSelectionState, 
            CardButtonPlayState cardButtonPlayState,
            ButtonSignals buttonSignals)
        {
            _cardButtonView = cardButtonView;
            _cardButtonSelectionState = cardButtonSelectionState;
            _cardButtonPlayState = cardButtonPlayState;
            _buttonSignals = buttonSignals;
            
            SubscribeEvents();
            
            _currentState = _cardButtonSelectionState;
        }
        
        private void SubscribeEvents()
        {
            _cardButtonView.Button.onClick.AddListener(OnButtonClick);
            _buttonSignals.OnSwitchState += OnSwitchState;
        }

        private void OnButtonClick()
        {
            _currentState.OnButtonClick();
        }
        
        private void OnSwitchState(CardButtonStateType newState)
        {
            _currentState = newState switch
            {
                CardButtonStateType.Selection => _cardButtonSelectionState,
                CardButtonStateType.Play => _cardButtonPlayState,
                _ => throw new ArgumentOutOfRangeException(nameof(newState), newState, null)
            };
        }
        
        private void UnsubscribeEvents()
        {
            _cardButtonView.Button.onClick.RemoveListener(OnButtonClick);
            _buttonSignals.OnSwitchState -= OnSwitchState;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}