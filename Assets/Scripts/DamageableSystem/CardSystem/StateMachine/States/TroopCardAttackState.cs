using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.CardSystem.View;
using Enums;

namespace DamageableSystem.CardSystem.StateMachine.States
{
    public class TroopCardAttackState : DamageableAttackState
    {
        private TroopCardView TroopCardView => (TroopCardView) DamageableView;
        
        public TroopCardAttackState(
            TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
        }
        
        public override void EnterState()
        {
            base.EnterState();
            TroopCardView.OnPlayAnimation?.Invoke(TroopCardAnimationType.Attack);
            TroopCardView.OnRotateToTarget?.Invoke();
        }
    }
}