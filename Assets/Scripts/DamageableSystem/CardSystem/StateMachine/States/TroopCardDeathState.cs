using DamageableSystem.CardSystem.StateMachine.Abstract;
using DamageableSystem.CardSystem.View;

namespace DamageableSystem.CardSystem.StateMachine.States
{
    public class TroopCardDeathState : CardDeathState
    {
        public TroopCardDeathState(
            TroopCardView troopCardView)
        {
            CardView = troopCardView;
        }
    }
}