using System;
using DamageableSystem.CardSystem.StateMachine.Abstract;
using DamageableSystem.CardSystem.StateMachine.States;
using DamageableSystem.CardSystem.View;
using Enums;
using Zenject;

namespace DamageableSystem.CardSystem.StateMachine.Managers
{
    public class BuildingCardStateManager : CardBaseStateManager,ITickable, IFixedTickable, IDisposable
    {
        private readonly BuildingCardView _buildingCardView;
        
        private readonly BuildingCardAttackState _buildingCardAttackState;
        
        private readonly BuildingCardIdleState _buildingCardIdleState;
        
        public BuildingCardStateManager(
            BuildingCardView buildingCardView,
            BuildingCardInitializationState ınitialisationState,
            BuildingCardIdleState buildingCardIdleState,
            BuildingCardAttackState buildingCardAttackState, 
            BuildingCardDeathState buildingCardDeathState)
        {
            _buildingCardView = buildingCardView;
            CardInitializationState = ınitialisationState;
            _buildingCardIdleState = buildingCardIdleState;
            _buildingCardAttackState = buildingCardAttackState;
            CardDeathState = buildingCardDeathState;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _buildingCardView.OnSwitchState += OnSwitchState;
        }
        
        public void Tick()
        {
            DamageableCurrentState?.UpdateState();
        }
        
        public void FixedTick()
        {
            DamageableCurrentState?.FixedUpdateState();
        }
        
        private void OnSwitchState(BuildingCardStateType newState)
        {
            DamageableCurrentState?.ExitState();
            
            DamageableCurrentState = newState switch
            {
                BuildingCardStateType.Initialization => CardInitializationState,
                BuildingCardStateType.Idle => _buildingCardIdleState,
                BuildingCardStateType.Attack => _buildingCardAttackState,
                BuildingCardStateType.Death => CardDeathState,
                _ => throw new ArgumentOutOfRangeException(nameof(newState), newState, null)
            };

            DamageableCurrentState.EnterState();
        }
        
        private void UnSubscribeEvents()
        {
            _buildingCardView.OnSwitchState -= OnSwitchState;
        }

        public void Dispose()
        {
            UnSubscribeEvents();
        }
    }
}