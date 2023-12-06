using DamageableSystem.Abstract.View;

namespace DamageableSystem.Abstract.StateMachine
{
    public abstract class DamageableAttackState : DamageableBaseState
    {
        protected DamageableView DamageableView;
        
        public override void EnterState()
        {
            DamageableView.OnSetAttackColliderEnabled?.Invoke(false);
        }
        
        public override void ExitState()
        {
            DamageableView.OnSetAttackColliderEnabled?.Invoke(true);
        }
    }
}