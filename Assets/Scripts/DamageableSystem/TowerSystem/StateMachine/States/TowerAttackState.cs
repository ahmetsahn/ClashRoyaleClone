using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.TowerSystem.View;

namespace DamageableSystem.TowerSystem.StateMachine.States
{
    public class TowerAttackState : DamageableAttackState
    {
        private TowerView TowerView => (TowerView) DamageableView;
        
        public TowerAttackState(
            TowerView towerView)
        {
            DamageableView = towerView;
        }

        public override void EnterState()
        {
            base.EnterState();
            TowerView.OnEnterAttackState?.Invoke();
        }

        public override void UpdateState()
        {
            TowerView.OnUpdateAttackState?.Invoke();
        }

        public override void ExitState()
        {
            base.ExitState();
            TowerView.OnExitAttackState?.Invoke();
        }
    }
}