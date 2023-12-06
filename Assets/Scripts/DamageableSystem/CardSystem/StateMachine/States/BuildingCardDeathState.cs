using DamageableSystem.CardSystem.StateMachine.Abstract;
using DamageableSystem.CardSystem.View;

namespace DamageableSystem.CardSystem.StateMachine.States
{
    public class BuildingCardDeathState : CardDeathState
    {
        public BuildingCardDeathState(
            BuildingCardView buildingCardView)
        {
            CardView = buildingCardView;
        }
    }
}