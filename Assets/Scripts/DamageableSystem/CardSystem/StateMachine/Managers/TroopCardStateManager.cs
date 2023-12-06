using System;
using DamageableSystem.CardSystem.StateMachine.Abstract;
using DamageableSystem.CardSystem.StateMachine.States;
using DamageableSystem.CardSystem.View;
using Enums;
using Zenject;

namespace DamageableSystem.CardSystem.StateMachine.Managers
{
    public class TroopCardStateManager : CardBaseStateManager, ITickable, IDisposable
    {
        private  readonly TroopCardView _troopCardView;
        
        private readonly TroopCardIdleState _troopCardIdleState;
        
        private readonly TroopCardMovementState _moveState;
        
        private readonly TroopCardAttackState _troopCardAttackState;
        
        public TroopCardStateManager(
            TroopCardInitializationState ınitialisationState,
            TroopCardIdleState idleState,
            TroopCardAttackState troopCardAttackState, 
            TroopCardMovementState moveState,
            TroopCardDeathState cardDeathState,
            TroopCardView troopCardView)
        {
            CardInitializationState = ınitialisationState;
            _troopCardIdleState = idleState;
            _troopCardAttackState = troopCardAttackState;
            _moveState = moveState;
            CardDeathState = cardDeathState;
            _troopCardView = troopCardView;
            
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _troopCardView.OnSwitchState += OnSwitchState;
            _troopCardView.OnInitializeTroopCard += OnInitializeTroopCard;
        }

        private void OnSwitchState(TroopCardStateType newState)
        {
            DamageableCurrentState?.ExitState();
            
            DamageableCurrentState = newState switch
            {
                TroopCardStateType.Initialization => CardInitializationState,
                TroopCardStateType.Idle => _troopCardIdleState,
                TroopCardStateType.Attack => _troopCardAttackState,
                TroopCardStateType.Movement => _moveState,
                TroopCardStateType.Death => CardDeathState,
                _ => throw new ArgumentOutOfRangeException(nameof(newState), newState, null)
            };

            DamageableCurrentState.EnterState();
        }
        
        private void OnInitializeTroopCard()
        {
            OnSwitchState(TroopCardStateType.Initialization);
        }
        
        public void Tick()
        {
            DamageableCurrentState?.UpdateState();
        }

        private void UnSubscribeEvents()
        {
            _troopCardView.OnSwitchState -= OnSwitchState;
            _troopCardView.OnInitializeTroopCard -= OnInitializeTroopCard;
        }

        public void Dispose()
        {
            UnSubscribeEvents();
        }
    }
}