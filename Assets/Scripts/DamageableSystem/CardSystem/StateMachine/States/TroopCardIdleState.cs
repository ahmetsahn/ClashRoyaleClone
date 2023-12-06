using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.CardSystem.View;
using Enums;

namespace DamageableSystem.CardSystem.StateMachine.States
{
    public class TroopCardIdleState : DamageableBaseState
    {
        private readonly TroopCardView _troopCardView;
        
        public TroopCardIdleState(TroopCardView troopCardView)
        {
            _troopCardView = troopCardView;
        }

        public override void EnterState()
        {
            _troopCardView.OnPlayAnimation?.Invoke(TroopCardAnimationType.Idle);
        }
    }
}