using DamageableSystem.TowerSystem.StateMachine.Manager.Abstract;
using DamageableSystem.TowerSystem.StateMachine.States;
using DamageableSystem.TowerSystem.View;
using Enums;

namespace DamageableSystem.TowerSystem.StateMachine.Manager
{
    public class AttackTowerStateManager : TowerStateManager
    {
        private readonly AttackTowerDeathState _attackTowerDeathState;
        
        public AttackTowerStateManager(
            TowerView towerView,
            TowerIdleState towerIdleState,
            TowerAttackState towerAttackState,
            AttackTowerDeathState attackTowerDeathState)
        {
            TowerView = towerView;
             
            TowerIdleState = towerIdleState;
            TowerAttackState = towerAttackState;
            _attackTowerDeathState = attackTowerDeathState;
        }
        
        protected override void OnSwitchState(TowerStateType newState)
        {
            CurrentState?.ExitState();
            
            CurrentState = newState switch
            {
                TowerStateType.Idle => TowerIdleState,
                TowerStateType.Attack => TowerAttackState,
                TowerStateType.Death => _attackTowerDeathState,
                _ => CurrentState
            };

            CurrentState?.EnterState();
        }
    }
}