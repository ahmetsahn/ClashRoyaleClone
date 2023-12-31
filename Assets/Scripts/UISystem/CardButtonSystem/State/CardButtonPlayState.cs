using UISystem.CardButtonSystem.View;
using UnityEngine;
using Util;

namespace UISystem.CardButtonSystem.State
{
    public class CardButtonPlayState : CardButtonBaseState
    {
        private readonly CardButtonView _cardButtonView;
        
        public CardButtonPlayState(
            CardButtonView cardButtonView)
        {
            _cardButtonView = cardButtonView;
        }

        public override void OnButtonClick()
        {
             ButtonSignals.OnIsSelectedCard?.Invoke(true);
             ButtonSignals.OnSetSelectedCardButton?.Invoke(_cardButtonView.CardButtonSo.CardType);
             ButtonSignals.OnSetSelectedCardElixirCost?.Invoke(_cardButtonView.CardButtonSo.UICardElixirData.ElixirCost);
             ButtonSignals.OnSetDeActiveAllCardPreview?.Invoke();
            _cardButtonView.CardPreview.SetActive(true);
            AudioSource.PlayClipAtPoint(_cardButtonView.ButtonClickSound, Utils.MainCamera.transform.position);
        }
    }
}