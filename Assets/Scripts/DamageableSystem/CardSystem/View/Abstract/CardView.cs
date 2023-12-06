using DamageableSystem.Abstract.View;
using Enums;
using PoolSystem;
using Signal;
using UnityEngine.Events;
using Zenject;

namespace DamageableSystem.CardSystem.View.Abstract
{
    public abstract class CardView : DamageableView
    {
        public CoreGameSignals CoreGameSignals;
        
        public CardPoolSignals CardPoolSignals;
        
        public UnityAction OnResetCard;
        public UnityAction OnSetInitialRotation;
        
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

        public class Factory : PlaceholderFactory<CardType, CardView> { }
    }
}