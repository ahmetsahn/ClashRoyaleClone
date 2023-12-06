using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.CardSystem.View.Abstract;
using Signal;

namespace DamageableSystem.CardSystem.StateMachine.Abstract
{
    public abstract class CardDeathState : DamageableBaseState
    {
        protected CardView CardView;
        
        public override void EnterState()
        {
            CardView.OnResetCard?.Invoke();
            CardView.OnSetAttackColliderEnabled?.Invoke(false);
            CardView.CardPoolSignals.OnReturnCardToPool?.Invoke(CardView);
        }
    }
}