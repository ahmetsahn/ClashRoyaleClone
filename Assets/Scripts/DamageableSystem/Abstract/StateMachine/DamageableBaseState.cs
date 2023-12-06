namespace DamageableSystem.Abstract.StateMachine
{
    public abstract class DamageableBaseState
    {
        public virtual void EnterState() { }
        
        public virtual void UpdateState() { }
        
        public virtual void FixedUpdateState() { }
        
        public virtual void ExitState() { }
    }
}