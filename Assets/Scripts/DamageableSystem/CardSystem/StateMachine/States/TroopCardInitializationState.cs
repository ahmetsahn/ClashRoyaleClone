using DamageableSystem.CardSystem.StateMachine.Abstract;
using DamageableSystem.CardSystem.View;
using Enums;

namespace DamageableSystem.CardSystem.StateMachine.States
{
    public class TroopCardInitializationState : CardInitializationState
    {
        private TroopCardView TroopCardView => (TroopCardView) CardView;
        
        public TroopCardInitializationState(
            TroopCardView troopCardView)
        {
            CardView = troopCardView;
        }
        protected override void ExitInitializationState()
        {
            base.ExitInitializationState();
            TroopCardView.OnSwitchState?.Invoke(TroopCardView.IsTargetInAttackRange() ? TroopCardStateType.Attack : TroopCardStateType.Movement);
        }
    }
}