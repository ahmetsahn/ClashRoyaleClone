using DamageableSystem.CardSystem.StateMachine.Abstract;
using DamageableSystem.CardSystem.View;
using Enums;

namespace DamageableSystem.CardSystem.StateMachine.States
{
    public class BuildingCardInitializationState : CardInitializationState
    {
        private BuildingCardView BuildingCardView => (BuildingCardView) CardView;
        
        public BuildingCardInitializationState(
            BuildingCardView buildingCardView)
        {
            CardView = buildingCardView;
            
            SubscribeEvents();
        }
        protected override void ExitInitializationState()
        {
            base.ExitInitializationState();
            BuildingCardView.OnSwitchState?.Invoke(BuildingCardView.IsTargetInAttackRange() ? BuildingCardStateType.Attack : BuildingCardStateType.Idle);
        }
    }
}