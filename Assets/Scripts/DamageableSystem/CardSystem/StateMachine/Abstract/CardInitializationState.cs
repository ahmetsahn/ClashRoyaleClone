using System;
using System.Threading;
using System.Threading.Tasks;
using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.CardSystem.View.Abstract;
using DG.Tweening;
using Zenject;

namespace DamageableSystem.CardSystem.StateMachine.Abstract
{
    public abstract class CardInitializationState : DamageableBaseState, IInitializable, IDisposable
    {
        protected CardView CardView;

        private CancellationTokenSource _cancellationTokenSource; 
        
        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            CardView.OnResetCard += CancelToken;
            CardView.CoreGameSignals.OnGameEnd += CancelToken;
        }

        public override async void EnterState()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Delay(TimeSpan.FromSeconds(CardView.DamageableSo.CardInstallationData.InstallationTime));
            ExitInitializationState();
        }

        protected virtual void ExitInitializationState()
        {
            CardView.OnSetAttackColliderEnabled?.Invoke(true);
            CardView.OnSetNewTarget?.Invoke();
        }

        private void CancelToken()
        {
            _cancellationTokenSource.Cancel();
        }
        
        private void UnsubscribeEvents()
        {
            CardView.OnResetCard -= CancelToken;
            CardView.CoreGameSignals.OnGameEnd -= CancelToken;
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
            CancelToken();
        }
    }
}