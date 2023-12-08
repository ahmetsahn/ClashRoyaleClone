using DamageableSystem.Abstract.View;
using Enums;
using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace DamageableSystem.TowerSystem.View
{
    public class TowerView : DamageableView
    {
        [field: SerializeField]
        public Transform TowerDefenderTransform { get; private set; }
        
        public UnityAction<TowerStateType> OnSwitchState;
        
        public UnityAction OnEnterAttackState;
        public UnityAction OnUpdateAttackState;
        public UnityAction OnExitAttackState;

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
            
            OnSwitchState?.Invoke(TowerStateType.Idle);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);
            
            if (IsDead)
            {
                OnSwitchState?.Invoke(TowerStateType.Death);
            }
        }
    }
}
