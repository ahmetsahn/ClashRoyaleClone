using Signal;
using UISystem.CardButtonSystem.View;

namespace UISystem.CardButtonSystem.State
{
    public class CardButtonPlayState : CardButtonBaseState
    {
        private readonly CardButtonView _cardButtonView;
        
        private readonly ButtonSignals _buttonSignals;
        
        
        public CardButtonPlayState(
            CardButtonView cardButtonView,
            ButtonSignals buttonSignals)
        {
            _cardButtonView = cardButtonView;
            _buttonSignals = buttonSignals;
        }

        public override void OnButtonClick()
        {
            _buttonSignals.OnIsSelectedCard?.Invoke(true);
            _buttonSignals.OnSetSelectedCardButton?.Invoke(_cardButtonView.CardButtonSo.CardType);
            _buttonSignals.OnSetSelectedCardElixirCost?.Invoke(_cardButtonView.CardButtonSo.UICardElixirData.ElixirCost);
            _buttonSignals.OnSetDeActiveAllCardPreview?.Invoke();
            _cardButtonView.CardPreview.SetActive(true);
        }
    }
}