using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.CardSystem.View;

namespace DamageableSystem.CardSystem.StateMachine.States
{
    public class BuildingCardAttackState : DamageableAttackState
    {
        private BuildingCardView BuildingCardView => (BuildingCardView) DamageableView;
        
        public BuildingCardAttackState(
            BuildingCardView damageableView)
        {
            DamageableView = damageableView;
        }

        public override void EnterState()
        {
            base.EnterState();
            BuildingCardView.OnEnterAttackState?.Invoke();
        }

        public override void UpdateState()
        {
            if(BuildingCardView.CurrentTarget == null)
            {
                return;
            }
            
            BuildingCardView.OnUpdateAttackState?.Invoke();
        }

        public override void FixedUpdateState()
        {
            BuildingCardView.OnFixedUpdateAttackState?.Invoke();
        }

        public override void ExitState()
        {
            base.ExitState();
            BuildingCardView.OnExitAttackState?.Invoke();
        }
    }
}