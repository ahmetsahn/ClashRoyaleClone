using System.Collections.Generic;
using DG.Tweening;
using Enums;
using Signal;
using UISystem.CardButtonSystem.View;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UISystem.Panels.Managers
{
    public class GamePanelManager : MonoBehaviour
    {
        [SerializeField]
        private CardButtonView[] cardButtonViews;
        
        [SerializeField]
        private Transform[] myCardsDestination;
        
        [SerializeField]
        private Transform[] opponentCardsDestination;
        
        [SerializeField]
        private Image shadowImage;

        private readonly Dictionary<CardButtonView, CardButtonView> _buttonDictionary = new();
        
        private UISignals _uiSignals;
        
        private ButtonSignals _buttonSignals;
        
        private CoreGameSignals _coreGameSignals;
        
        private int _currentButtonInteractIndex;
        private int _currentButtonSelectIndex;
        
        
        [Inject]
        public void Construct(
            UISignals uiSignals,
            ButtonSignals buttonSignals,
            CoreGameSignals coreGameSignals)
        {
            _uiSignals = uiSignals;
            _buttonSignals = buttonSignals;
            _coreGameSignals = coreGameSignals;
        }

        private void Awake()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            for (int i = 0; i < cardButtonViews.Length; i += 2)
            {
                _buttonDictionary.Add(cardButtonViews[i], cardButtonViews[i + 1]);
                _buttonDictionary.Add(cardButtonViews[i + 1], cardButtonViews[i]);
            }
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _buttonSignals.OnSetInteractableTwoCardButtons += OnSetInteractableTwoCardButtons;
            _buttonSignals.OnSelectedCardButton += OnSelectedCardButton;
        }
        
        private void OnSetInteractableTwoCardButtons()
        {
            if(_currentButtonInteractIndex == cardButtonViews.Length - 1) return;
            
            cardButtonViews[_currentButtonInteractIndex].Button.interactable = true;
            _currentButtonInteractIndex++;
            
            cardButtonViews[_currentButtonInteractIndex].Button.interactable = true;
            if(_currentButtonInteractIndex < cardButtonViews.Length - 1)
                _currentButtonInteractIndex++;
        }

        private void OnSelectedCardButton(CardButtonView button)
        {
            button.transform.DOMove(myCardsDestination[_currentButtonSelectIndex].position, 1f);
            button.Button.interactable = false;
            _buttonDictionary[button].transform.DOMove(opponentCardsDestination[_currentButtonSelectIndex].position, 1f);
            _buttonDictionary[button].Button.interactable = false;
            
            _buttonSignals.OnAddToAICardList?.Invoke(_buttonDictionary[button].CardType);
            
            _currentButtonSelectIndex++;

            if (_currentButtonSelectIndex != cardButtonViews.Length / 2)
            {
                return;
            }
            
            CloseShadowImageAndStartGame();
        }

        private void CloseShadowImageAndStartGame()
        {
            shadowImage.DOFade(0f, 1f).onComplete += () =>
            {
                shadowImage.gameObject.SetActive(false);
                _coreGameSignals.OnPlayStart?.Invoke();
                _uiSignals.OnOpenPanel?.Invoke(UIPanelType.ElixirBarPanel, 1);
                _buttonSignals.OnSwitchState?.Invoke(CardButtonStateType.Play);
            };
        }

        private void UnsubscribeEvents()
        {
            _buttonSignals.OnSetInteractableTwoCardButtons -= OnSetInteractableTwoCardButtons;
            _buttonSignals.OnSelectedCardButton -= OnSelectedCardButton;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}