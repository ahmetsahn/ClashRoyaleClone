using System.Threading;
using DamageableSystem.Abstract.View;
using Enums;
using PoolSystem;
using Signal;
using Unity.VisualScripting;
using UnityEngine.Events;
using Zenject;

namespace DamageableSystem.CardSystem.View.Abstract
{
    public abstract class CardView : DamageableView
    {
        private CoreGameSignals _coreGameSignals;
        
        public CardPoolSignals CardPoolSignals;
        
        public UnityAction OnSetInitialRotation;
        
        public CancellationTokenSource CancellationTokenSource; 
        
        [Inject]
        private void Construct(
            CoreGameSignals coreGameSignals,
            CardPoolSignals cardPoolSignals)
        {
            _coreGameSignals = coreGameSignals;
            CardPoolSignals = cardPoolSignals;
        }

        protected override void InitializeDamageable()
        {
            base.InitializeDamageable();
            OnSetInitialRotation?.Invoke();
            InitializeToken();
        }
        
        private void InitializeToken()
        {
            CancellationTokenSource = new CancellationTokenSource();
        }
        
        private void DestroyToken()
        {
            if(CancellationTokenSource == null)
            {
                return;
            }
            
            CancellationTokenSource.Cancel();
            CancellationTokenSource.Dispose();
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            _coreGameSignals.OnGameEnd += OnReturnToPool;
        }
        
        private void OnReturnToPool()
        {
            CardPoolSignals.OnReturnCardToPool?.Invoke(this);
        }
        
        private void SetTargetNull()
        {
            CurrentTarget = null;
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            _coreGameSignals.OnGameEnd -= OnReturnToPool;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DestroyToken();
            SetTargetNull();
        }

        public class Factory : PlaceholderFactory<CardType, CardView> { }
    }
}