using DamageableSystem.Abstract.View;
using Signal;
using UnityEngine;
using Zenject;

namespace DamageableSystem.Abstract.Handler.AttackHandler
{
    public abstract class DamageableBaseAttackHandler : MonoBehaviour
    {
        protected DamageableView DamageableView;
        
        protected DamageableSignals DamageableSignals;
        
        protected ParticlePoolSignals ParticlePoolSignals;
        
        protected GameObject AttackParticle;
        
        [Inject]
        private void Construct(
            DamageableSignals damageableSignals,
            ParticlePoolSignals particlePoolSignals)
        {
            DamageableSignals = damageableSignals;
            ParticlePoolSignals = particlePoolSignals;
        }
        
        protected virtual void ControlAndDamageTheTarget()
        {
            if(DamageableView.CurrentTarget == null)
            {
                return;
            }
            
            PlayAttackParticle();
            
            DamageableView.CurrentTarget.TakeDamage(DamageableView.DamageableSo.DamageableAttackData.AttackDamage);
            
            ControlAndFireOnTargetDestroyed();
        }

        protected void ControlAndFireOnTargetDestroyed()
        {
            if(!DamageableView.CurrentTarget.IsDead)
            {
                return;
            }
            
            DamageableSignals.OnTargetDestroyed?.Invoke(DamageableView.CurrentTarget);
        }

        protected void PlayAttackParticle()
        {
            SetAttackParticle();
            SetAttackParticlePosition();
            SetActiveAttackParticle();
        }

        private void SetAttackParticle()
        {
            AttackParticle = ParticlePoolSignals.OnGetParticle?.Invoke(DamageableView.DamageableSo.DamageableAttackData.AttackParticleType);
        }
        
        protected virtual void SetAttackParticlePosition()
        {
            AttackParticle.transform.position = DamageableView.CurrentTarget.Transform.position;
        }
        
        private void SetActiveAttackParticle()
        {
            AttackParticle.SetActive(true);
        }
       
        protected void SetDisabledAttackParticle()
        {
            AttackParticle.SetActive(false);
        }
    }
}