using System.Threading.Tasks;
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
            InvokeRepeating(nameof(StartLifeCycle), DamageableSo.CardInstallationData.InstallationTime, 0.01f);
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
        
        private void StartLifeCycle()
        {
            TakeDamage(1/DamageableSo.BuildingCardLifeTime.LifeTime);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            CancelInvoke(nameof(StartLifeCycle));
        }
    }
}