using System;
using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.TowerSystem.StateMachine.States;
using DamageableSystem.TowerSystem.View;
using Enums;
using Zenject;

namespace DamageableSystem.TowerSystem.StateMachine.Manager.Abstract
{
    public abstract class TowerStateManager : IInitializable, ITickable, IDisposable
    {
        protected TowerView TowerView;
        
        protected TowerIdleState TowerIdleState;
        
        protected TowerAttackState TowerAttackState;

        protected DamageableBaseState CurrentState;
        
        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            TowerView.OnSwitchState += OnSwitchState;
        }
        
        public void Tick()
        {
            CurrentState?.UpdateState();
        }
        
        protected abstract void OnSwitchState(TowerStateType newState);
        
        private void UnSubscribeEvents()
        {
            TowerView.OnSwitchState -= OnSwitchState;
        }

        public void Dispose()
        {
            UnSubscribeEvents();
        }
    }
}