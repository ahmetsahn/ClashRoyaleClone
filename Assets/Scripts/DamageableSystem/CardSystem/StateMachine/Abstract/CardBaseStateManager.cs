using DamageableSystem.Abstract.StateMachine;

namespace DamageableSystem.CardSystem.StateMachine.Abstract
{
    public abstract class CardBaseStateManager
    {
        protected DamageableBaseState DamageableCurrentState;
        
        protected CardInitializationState CardInitializationState;
        
        protected CardDeathState CardDeathState;
    }
}