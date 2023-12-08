using DamageableSystem.TowerSystem.StateMachine.Manager.Abstract;
using DamageableSystem.TowerSystem.StateMachine.States;
using DamageableSystem.TowerSystem.View;
using Enums;

namespace DamageableSystem.TowerSystem.StateMachine.Manager
{
    public class BossTowerStateManager : TowerStateManager
    {
        private readonly BossTowerDeathState _bossTowerDeathState;
        
        public BossTowerStateManager(
            TowerView towerView,
            BossTowerDeathState bossTowerDeathState)
        {
            TowerView = towerView;
            
            _bossTowerDeathState = bossTowerDeathState;
        }
        
        protected override void OnSwitchState(TowerStateType newState)
        {
            CurrentState?.ExitState();
            
            CurrentState = newState switch
            {
                TowerStateType.Idle => TowerIdleState,
                TowerStateType.Attack => TowerAttackState,
                TowerStateType.Death => _bossTowerDeathState,
                _ => CurrentState
            };

            CurrentState?.EnterState();
        }
    }
}