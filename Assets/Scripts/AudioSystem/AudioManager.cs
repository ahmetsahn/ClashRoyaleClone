using System;
using Signal;
using UnityEngine;
using Util;
using Zenject;

namespace AudioSystem
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;
        
        [SerializeField]
        private AudioClip winSound;
        
        [SerializeField]
        private AudioClip loseSound;
        
        private CoreGameSignals _coreGameSignals;
        
        [Inject]
        public void Construct(
            CoreGameSignals coreGameSignals)
        {
            _coreGameSignals = coreGameSignals;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _coreGameSignals.OnPlayStart += OnPlayStartSound;
            _coreGameSignals.OnWin += OnWinSound;
            _coreGameSignals.OnLose += OnLoseSound;
            _coreGameSignals.OnGameEnd += OnStopSound;
        }
        
        private void OnPlayStartSound()
        {
            audioSource.Play();
        }
        
        private void OnWinSound()
        {
            AudioSource.PlayClipAtPoint(winSound, Utils.MainCamera.transform.position);
        }
        
        private void OnLoseSound()
        {
            AudioSource.PlayClipAtPoint(loseSound, Utils.MainCamera.transform.position);
        }
        
        private void OnStopSound()
        {
            audioSource.Stop();
        }
        
        private void UnsubscribeEvents()
        {
            _coreGameSignals.OnPlayStart -= OnPlayStartSound;
            _coreGameSignals.OnWin -= OnWinSound;
            _coreGameSignals.OnLose -= OnLoseSound;
            _coreGameSignals.OnGameEnd -= OnStopSound;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}