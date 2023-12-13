using UISystem.CardButtonSystem.View;
using UnityEngine;
using Util;

namespace UISystem.CardButtonSystem.State
{
    public class CardButtonSelectionState : CardButtonBaseState
    {
        private readonly CardButtonView _cardButtonView;

        public CardButtonSelectionState(
            CardButtonView cardButtonView)
        {
            _cardButtonView = cardButtonView;
        }
        
        public override void OnButtonClick()
        {
            ButtonSignals.OnSelectedCardButton?.Invoke(_cardButtonView);
            ButtonSignals.OnSetInteractableTwoCardButtons?.Invoke();
            AudioSource.PlayClipAtPoint(_cardButtonView.ButtonClickSound, Utils.MainCamera.transform.position);
        }
    }
}