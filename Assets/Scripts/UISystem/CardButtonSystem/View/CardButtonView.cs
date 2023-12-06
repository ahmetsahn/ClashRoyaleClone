using Data.ScriptableObject.CardButton;
using Enums;
using Signal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UISystem.CardButtonSystem.View
{
    public class CardButtonView : MonoBehaviour
    {
        [field: SerializeField]
        public CardType CardType { get; private set; }
        
        [field: SerializeField]
        public CardButtonSo CardButtonSo { get; private set; }
        
        [field: SerializeField]
        public Button Button { get; private set; }
        
        [field: SerializeField]
        public Image CooldownImage { get; private set; }
        
        [field: SerializeField]
        public GameObject CardPreview { get; private set; }
        
        [SerializeField]
        private TextMeshProUGUI elixirText;
        
        private InputSignals _inputSignals;
        
        private ButtonSignals _buttonSignals;
        
        [Inject]
        public void Construct(
            InputSignals inputSignals,
            ButtonSignals buttonSignals)
        {
            _inputSignals = inputSignals;
            _buttonSignals = buttonSignals;
        }

        private void Awake()
        {
            elixirText.text = CardButtonSo.UICardElixirData.ElixirCost.ToString();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _inputSignals.OnClickMouseButtonDown += OnSetDeActiveAllCardPreview;
            _inputSignals.OnClickMouseButtonDown += OnSetUnselectedCard;
            _buttonSignals.OnCheckButtonInteractable += OnCheckButtonInteractable;
            _buttonSignals.OnSetDeActiveAllCardPreview += OnSetDeActiveAllCardPreview;
        }
        
        private void OnSetDeActiveAllCardPreview()
        {
            CardPreview.SetActive(false);
        }
        
        private void OnSetUnselectedCard()
        {
            _buttonSignals.OnIsSelectedCard?.Invoke(false);
        }

        private void OnCheckButtonInteractable(int elixirCount)
        {
            Button.interactable = elixirCount >= CardButtonSo.UICardElixirData.ElixirCost;
        }
        
        private void UnsubscribeEvents()
        {
            _inputSignals.OnClickMouseButtonDown -= OnSetDeActiveAllCardPreview;
            _inputSignals.OnClickMouseButtonDown -= OnSetUnselectedCard;
            _buttonSignals.OnCheckButtonInteractable -= OnCheckButtonInteractable;
            _buttonSignals.OnSetDeActiveAllCardPreview -= OnSetDeActiveAllCardPreview;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}