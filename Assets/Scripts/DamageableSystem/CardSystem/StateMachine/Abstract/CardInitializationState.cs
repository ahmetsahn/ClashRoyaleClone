using System;
using System.Threading.Tasks;
using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.CardSystem.View.Abstract;
namespace DamageableSystem.CardSystem.StateMachine.Abstract
{
    public abstract class CardInitializationState : DamageableBaseState
    {
        protected CardView CardView;
        
        public override async void EnterState()
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(CardView.DamageableSo.CardInstallationData.InstallationTime), CardView.CancellationTokenSource.Token);
                ExitInitializationState();
            }
            catch
            {
                // ignored
            }
        }
        
        protected virtual void ExitInitializationState()
        {
            CardView.OnSetAttackColliderEnabled?.Invoke(true);
            CardView.OnSetNewTarget?.Invoke();
        }
    }
}