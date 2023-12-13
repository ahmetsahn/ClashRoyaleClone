using System;
using Enums;
using Signal;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Zenject;

namespace UISystem
{
    public class UIEventSubscriber : MonoBehaviour
    {
        [SerializeField] 
        private UIEventSubscriptionType subscriptionType;
        
        [SerializeField]
        private Button button;
        
        [SerializeField]
        private AudioClip selectSound;
        
        private ButtonSignals _buttonSignals;
        
        [Inject]
        private void Construct(
            ButtonSignals buttonSignals)
        {
            _buttonSignals = buttonSignals;
        }
        
        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            switch (subscriptionType)
            {
                case UIEventSubscriptionType.Play:
                {
                    button.onClick.AddListener(_buttonSignals.OnPlayButtonClicked.Invoke);
                    button.onClick.AddListener(PlaySelectSound);
                    break;
                }
                
                case UIEventSubscriptionType.Menu:
                {
                    button.onClick.AddListener(_buttonSignals.OnMenuButtonClicked.Invoke);
                    button.onClick.AddListener(PlaySelectSound);
                    break;
                }
                
                case UIEventSubscriptionType.Exit:
                {
                    button.onClick.AddListener(_buttonSignals.OnExitButtonClicked.Invoke);
                    button.onClick.AddListener(PlaySelectSound);
                    break;
                }
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void PlaySelectSound()
        {
            AudioSource.PlayClipAtPoint(selectSound, Utils.MainCamera.transform.position);
        }
        
        private void UnsubscribeEvents()
        {
            switch (subscriptionType)
            {
                case UIEventSubscriptionType.Play:
                {
                    button.onClick.RemoveListener(_buttonSignals.OnPlayButtonClicked.Invoke);
                    button.onClick.RemoveListener(PlaySelectSound);
                    break;
                }
                
                case UIEventSubscriptionType.Menu:
                {
                    button.onClick.RemoveListener(_buttonSignals.OnMenuButtonClicked.Invoke);
                    button.onClick.RemoveListener(PlaySelectSound);
                    break;
                }
                
                case UIEventSubscriptionType.Exit:
                {
                    button.onClick.RemoveListener(_buttonSignals.OnExitButtonClicked.Invoke);
                    button.onClick.RemoveListener(PlaySelectSound);
                    break;
                }
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}