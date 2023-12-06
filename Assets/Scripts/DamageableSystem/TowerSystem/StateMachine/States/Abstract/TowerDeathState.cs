using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.TowerSystem.View;

namespace DamageableSystem.TowerSystem.StateMachine.States.Abstract
{
    public abstract class TowerDeathState : DamageableBaseState
    {
        protected TowerView TowerView;
        public override void EnterState()
        {
            TowerView.gameObject.SetActive(false);
        }
    }
}