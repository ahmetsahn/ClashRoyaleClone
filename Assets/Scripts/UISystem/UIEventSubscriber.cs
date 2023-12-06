using System;
using Enums;
using Signal;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UISystem
{
    public class UIEventSubscriber : MonoBehaviour
    {
        [SerializeField] 
        private UIEventSubscriptionType subscriptionType;
        
        [SerializeField]
        private Button button;
        
        private ButtonSignals _buttonSignals;
        
        [Inject]
        private void Construct(ButtonSignals buttonSignals)
        {
            _buttonSignals = buttonSignals;
        }
        
        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            switch (subscriptionType)
            {
                case UIEventSubscriptionType.Play:
                {
                    button.onClick.AddListener(_buttonSignals.OnPlayButtonClicked.Invoke);
                    break;
                }
                
                case UIEventSubscriptionType.Menu:
                {
                    button.onClick.AddListener(_buttonSignals.OnMenuButtonClicked.Invoke);
                    break;
                }
                
                case UIEventSubscriptionType.Exit:
                {
                    button.onClick.AddListener(_buttonSignals.OnExitButtonClicked.Invoke);
                    break;
                }
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UnsubscribeEvents()
        {
            switch (subscriptionType)
            {
                case UIEventSubscriptionType.Play:
                {
                    button.onClick.RemoveListener(_buttonSignals.OnPlayButtonClicked.Invoke);
                    break;
                }
                
                case UIEventSubscriptionType.Menu:
                {
                    button.onClick.RemoveListener(_buttonSignals.OnMenuButtonClicked.Invoke);
                    break;
                }
                
                case UIEventSubscriptionType.Exit:
                {
                    button.onClick.RemoveListener(_buttonSignals.OnExitButtonClicked.Invoke);
                    break;
                }
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}