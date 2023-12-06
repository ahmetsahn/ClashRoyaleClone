using System;
using LevelSystem.View;
using Signal;
using Object = UnityEngine.Object;

namespace LevelSystem.Handler
{
    public class LevelHandler : IDisposable
    {
        private readonly LevelView.Factory _levelFactory;
        
        private readonly LevelSignals _levelSignals;
        
        private readonly CoreGameSignals _coreGameSignals;
        
        private LevelView _currentLevel;
        
        public LevelHandler(
            LevelView.Factory levelFactory,
            LevelSignals levelSignals,
            CoreGameSignals coreGameSignals)
        {
            _levelFactory = levelFactory;
            _levelSignals = levelSignals;
            _coreGameSignals = coreGameSignals;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _levelSignals.OnCreateLevel += OnCreateLevel;
            _coreGameSignals.OnGameEnd += OnDestroyLevel;
        }
        
        private void OnCreateLevel()
        {
            _currentLevel = _levelFactory.Create();
        }

        private void OnDestroyLevel()
        {
            Object.Destroy(_currentLevel.gameObject);
        }
        
        private void UnsubscribeEvents()
        {
            _levelSignals.OnCreateLevel -= OnCreateLevel;
            _coreGameSignals.OnGameEnd -= OnDestroyLevel;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}