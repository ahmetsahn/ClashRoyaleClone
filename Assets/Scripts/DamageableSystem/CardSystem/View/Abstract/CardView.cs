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
        public CoreGameSignals CoreGameSignals;
        
        public CardPoolSignals CardPoolSignals;
        
        public UnityAction OnSetInitialRotation;
        
        public CancellationTokenSource CancellationTokenSource; 
        
        [Inject]
        private void Construct(
            CoreGameSignals coreGameSignals,
            CardPoolSignals cardPoolSignals)
        {
            CoreGameSignals = coreGameSignals;
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
            CancellationTokenSource = null;
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();
            CoreGameSignals.OnGameEnd += OnReturnToPool;
        }
        
        private void OnReturnToPool()
        {
            CardPoolSignals.OnReturnCardToPool?.Invoke(this);
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();
            CoreGameSignals.OnGameEnd -= OnReturnToPool;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DestroyToken();
        }

        public class Factory : PlaceholderFactory<CardType, CardView> { }
    }
}