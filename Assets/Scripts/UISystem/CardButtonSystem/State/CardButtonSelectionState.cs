using Signal;
using UISystem.CardButtonSystem.View;

namespace UISystem.CardButtonSystem.State
{
    public class CardButtonSelectionState : CardButtonBaseState
    {
        private readonly CardButtonView _cardButtonView;
        
        private readonly ButtonSignals _buttonSignals;

        public CardButtonSelectionState(
            CardButtonView cardButtonView,
            ButtonSignals buttonSignals)
        {
            _cardButtonView = cardButtonView;
            _buttonSignals = buttonSignals;
        }
        
        public override void OnButtonClick()
        {
            _buttonSignals.OnSelectedCardButton?.Invoke(_cardButtonView);
            _buttonSignals.OnSetInteractableTwoCardButtons?.Invoke();
        }
    }
}