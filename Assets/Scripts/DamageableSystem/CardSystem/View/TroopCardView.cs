using DamageableSystem.CardSystem.View.Abstract;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace DamageableSystem.CardSystem.View
{
    public class TroopCardView : CardView
    {
        [field: SerializeField]
        public Animator Animator { get; private set; }
        
        [field: SerializeField]
        public NavMeshAgent NavMeshAgent { get; private set; }
        
        public UnityAction<TroopCardAnimationType> OnPlayAnimation;
        
        public UnityAction<TroopCardStateType> OnSwitchState;
        
        public UnityAction OnRotateToTarget;
        
        public UnityAction OnInitializeTroopCard;
        
        protected override void InitializeDamageable()
        {
            base.InitializeDamageable();
            OnInitializeTroopCard?.Invoke();
        }

        protected override void OnTargetDestroyed(IDamageable target)
        {
            if (target != CurrentTarget)
            {
                return;
            }
            
            OnSetNewTarget?.Invoke();
            
            if (CurrentTarget == null)
            {
                OnSwitchState?.Invoke(TroopCardStateType.Idle);
                return;
            }
            
            if (IsTargetInAttackRange())
            {
                OnRotateToTarget?.Invoke();
                return;
            }
            
            OnSwitchState?.Invoke(TroopCardStateType.Movement);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (IsDead)
            {
                OnSwitchState?.Invoke(TroopCardStateType.Death);
            }
        }
    }
}