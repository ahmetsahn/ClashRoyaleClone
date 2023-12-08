using System;
using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.CardSystem.View.Abstract;
using DG.Tweening;
using Zenject;

namespace DamageableSystem.CardSystem.StateMachine.Abstract
{
    public abstract class CardInitializationState : DamageableBaseState, IDisposable
    {
        protected CardView CardView;

        private Sequence _mySequence;
        
        protected void SubscribeEvents()
        {
            CardView.OnResetCard += KillSequence;
            CardView.CoreGameSignals.OnGameEnd += KillSequence;
        }

        public override void EnterState()
        {
            _mySequence = DOTween.Sequence();
            _mySequence.PrependInterval(CardView.DamageableSo.CardInstallationData.InstallationTime);
            _mySequence.AppendCallback(ExitInitializationState);
        }

        protected virtual void ExitInitializationState()
        {
            CardView.OnSetAttackColliderEnabled?.Invoke(true);
            CardView.OnSetNewTarget?.Invoke();
        }

        private void KillSequence()
        {
            _mySequence?.Kill();
        }
        
        private void UnsubscribeEvents()
        {
            CardView.OnResetCard -= KillSequence;
            CardView.CoreGameSignals.OnGameEnd -= KillSequence;
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}