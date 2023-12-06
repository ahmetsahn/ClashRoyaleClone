using Signal;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UISystem.Panels.Managers
{
    public class ElixirBarPanelManager : MonoBehaviour
    {
        [SerializeField] 
        private Image elixirBarFillImage;
        
        [SerializeField] 
        private TextMeshProUGUI elixirCountText;
        
        private InputSignals _ınputSignals;
        
        private ButtonSignals _buttonSignals;

        private int _currentElixirCount;
        private int _selectedCardElixirCost;
        
        private bool _isElixirBarFull;
        
        [Inject]
        public void Construct(
            InputSignals ınputSignals,
            ButtonSignals buttonSignals)
        {
            _ınputSignals = ınputSignals;
            _buttonSignals = buttonSignals;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _ınputSignals.OnClickMouseButtonDown += OnHandleReduceElixirCount;
            _buttonSignals.OnSetSelectedCardElixirCost += OnSetSelectedCardElixirCost;
        }

        private void Start()
        {
            InvokeRepeating(nameof(HandleIncrementElixirCount), 1, 1);
        }

        private void Update()
        {
            if(_isElixirBarFull) return;

            IncreaseElixirBarImageFillAmount();
        }
        
        private void OnSetSelectedCardElixirCost(int cardElixirCost)
        {
            _selectedCardElixirCost = cardElixirCost;
        }
        
        private void HandleIncrementElixirCount()
        {
            IncreaseElixirCount();
            UpdateElixirCountText();
            ControlIncreaseElixirCount();
            NotifyButtonInteractable();
        }
        
        private void IncreaseElixirCount()
        {
            _currentElixirCount++;
        }
        
        private void UpdateElixirCountText()
        {
            elixirCountText.text = _currentElixirCount.ToString();
        }

        private void ControlIncreaseElixirCount()
        {
            if (_currentElixirCount != 10) return;
            
            _isElixirBarFull = true;
            CancelInvoke(nameof(HandleIncrementElixirCount));
        }
        
        private void NotifyButtonInteractable()
        {
            _buttonSignals.OnCheckButtonInteractable?.Invoke(_currentElixirCount);
        }

        private void IncreaseElixirBarImageFillAmount()
        {
            elixirBarFillImage.fillAmount += 0.1f * Time.deltaTime;
        }

        private void OnHandleReduceElixirCount()
        {
            ReduceElixirCount(_selectedCardElixirCost);
            UpdateElixirCountText();
            ReduceElixirBarImageFillAmount(_selectedCardElixirCost);
            ControlReduceElixirCount();
            NotifyButtonInteractable();
        }
        
        private void ReduceElixirCount(int elixirCost)
        {
            _currentElixirCount -= elixirCost;
        }
        
        private void ReduceElixirBarImageFillAmount(int cardElixirCost)
        {
            elixirBarFillImage.fillAmount -= cardElixirCost * 0.1f;
        }

        private void ControlReduceElixirCount()
        {
            if (!_isElixirBarFull) return;
            InvokeRepeating(nameof(HandleIncrementElixirCount), 1, 1);
            _isElixirBarFull = false;
        }
        
        private void UnsubscribeEvents()
        {
            _ınputSignals.OnClickMouseButtonDown -= OnHandleReduceElixirCount;
            _buttonSignals.OnSetSelectedCardElixirCost -= OnSetSelectedCardElixirCost;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}