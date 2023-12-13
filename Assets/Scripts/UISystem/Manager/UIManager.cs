using System;
using Enums;
using Signal;
using UnityEngine;

namespace UISystem.Manager
{
    public class UIManager : IDisposable
    {
        private readonly UISignals _uiSignals;
        
        private readonly ButtonSignals _buttonSignals;
        
        private readonly LevelSignals _levelSignals;
        
        private readonly CoreGameSignals _coreGameSignals;
        
        
        public UIManager(
            UISignals uiSignals,
            ButtonSignals buttonSignals,
            LevelSignals levelSignals,
            CoreGameSignals coreGameSignals)
        {
            _uiSignals = uiSignals;
            _buttonSignals = buttonSignals;
            _levelSignals = levelSignals;
            _coreGameSignals = coreGameSignals;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _buttonSignals.OnPlayButtonClicked += OnPlayButtonClicked;
            _buttonSignals.OnMenuButtonClicked += OnMenuButtonClicked;
            _buttonSignals.OnExitButtonClicked += OnExitButtonClicked;
            _coreGameSignals.OnGameEnd += OnGameEnd;
            _coreGameSignals.OnWin += OnWin;
            _coreGameSignals.OnLose += OnLose;
            
        }

        private void OnPlayButtonClicked()
        {
            _levelSignals.OnCreateLevel.Invoke();
            _uiSignals.OnClosePanel.Invoke(0);
            _uiSignals.OnOpenPanel.Invoke(UIPanelType.GamePanel, 0);
            _buttonSignals.OnSetInteractableTwoCardButtons.Invoke();
        }
        
        private void OnMenuButtonClicked()
        {
            _uiSignals.OnClosePanel.Invoke(0);
            _uiSignals.OnOpenPanel.Invoke(UIPanelType.MenuPanel, 0);
        }
        
        private void OnExitButtonClicked()
        {
            Application.Quit();
        }
        
        private void OnGameEnd()
        {
            _uiSignals.OnCloseAllPanels.Invoke();
        }
        
        private void OnWin()
        {
            _uiSignals.OnOpenPanel.Invoke(UIPanelType.WinPanel, 0);
        }
        
        private void OnLose()
        {
            _uiSignals.OnOpenPanel.Invoke(UIPanelType.GameOverPanel, 0);
        }
        
        private void UnsubscribeEvents()
        {
            _buttonSignals.OnPlayButtonClicked -= OnPlayButtonClicked;
            _buttonSignals.OnMenuButtonClicked -= OnMenuButtonClicked;
            _buttonSignals.OnExitButtonClicked -= OnExitButtonClicked;
            _coreGameSignals.OnGameEnd -= OnGameEnd;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}