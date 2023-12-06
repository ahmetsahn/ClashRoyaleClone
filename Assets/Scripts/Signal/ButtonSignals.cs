using Enums;
using UISystem.CardButtonSystem.View;
using UnityEngine.Events;

namespace Signal
{
    public class ButtonSignals
    {
        public UnityAction OnPlayButtonClicked;
        public UnityAction OnMenuButtonClicked;
        public UnityAction OnExitButtonClicked;
        public UnityAction OnSetInteractableTwoCardButtons;
        public UnityAction OnSetDeActiveAllCardPreview;
        
        public UnityAction<bool> OnIsSelectedCard;
        
        public UnityAction<int> OnSetSelectedCardElixirCost;
        public UnityAction<int> OnCheckButtonInteractable;
        
        public UnityAction<CardType> OnSetSelectedCardButton;
        public UnityAction<CardType> OnAddToAICardList;
        
        public UnityAction<CardButtonStateType> OnSwitchState;
        
        public UnityAction<CardButtonView> OnSelectedCardButton;
    }
}