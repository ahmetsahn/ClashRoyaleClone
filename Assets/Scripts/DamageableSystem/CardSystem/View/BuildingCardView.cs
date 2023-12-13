using DamageableSystem.CardSystem.View.Abstract;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace DamageableSystem.CardSystem.View
{
    public class BuildingCardView : CardView
    {
        [field: SerializeField]
        public AudioSource AudioSource { get; private set; }
        
        public UnityAction<BuildingCardStateType> OnSwitchState;
        
        public UnityAction OnEnterAttackState;
        public UnityAction OnUpdateAttackState;
        public UnityAction OnFixedUpdateAttackState;
        public UnityAction OnExitAttackState;

        protected override void InitializeDamageable()
        {
            base.InitializeDamageable();
            OnSwitchState?.Invoke(BuildingCardStateType.Initialization);
        }

        protected override void OnTargetDestroyed(IDamageable target)
        {
            if (target != CurrentTarget)
            {
                return;
            }

            OnSetNewTarget?.Invoke();
            
            if (IsTargetInAttackRange())
            {
                return;
            }
            
            OnSwitchState?.Invoke(BuildingCardStateType.Idle);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            if (IsDead)
            {
                OnSwitchState?.Invoke(BuildingCardStateType.Death);
            }
        }
    }
}