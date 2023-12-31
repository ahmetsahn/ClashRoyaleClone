using DamageableSystem.Abstract.View;
using ParticleSystem.View;
using Signal;
using UnityEngine;
using Util;
using Zenject;

namespace DamageableSystem.Abstract.Handler.AttackHandler
{
    public abstract class DamageableBaseAttackHandler : MonoBehaviour
    {
        protected DamageableView DamageableView;
        
        private DamageableSignals _damageableSignals;
        
        private ParticlePoolSignals _particlePoolSignals;
        
        protected ParticleView AttackParticle;
        
        
        [Inject]
        private void Construct(
            DamageableSignals damageableSignals,
            ParticlePoolSignals particlePoolSignals)
        {
            _damageableSignals = damageableSignals;
            _particlePoolSignals = particlePoolSignals;
        }
        
        protected virtual void ControlAndDamageTheTarget()
        {
            if(DamageableView.CurrentTarget == null)
            {
                return;
            }
            DamageableView.CurrentTarget.TakeDamage(DamageableView.DamageableSo.DamageableAttackData.AttackDamage);
            PlayAttackParticle();
            ControlAndFireOnTargetDestroyed();
        }

        protected void ControlAndFireOnTargetDestroyed()
        {
            if(DamageableView.CurrentTarget == null)
            {
                return;
            }
            
            if(!DamageableView.CurrentTarget.IsDead)
            {
                return;
            }
            
            _damageableSignals.OnTargetDestroyed?.Invoke(DamageableView.CurrentTarget);
        }

        protected void PlayAttackParticle()
        {
            SetAttackParticle();
            SetAttackParticlePosition();
            SetActiveAttackParticle();
        }
        
        protected void PlayAttackSound()
        {
            AudioSource.PlayClipAtPoint(DamageableView.DamageableSo.DamageableAttackData.AttackSound, Utils.MainCamera.transform.position);
        }

        private void SetAttackParticle()
        {
            AttackParticle = _particlePoolSignals.OnGetParticle?.Invoke(DamageableView.DamageableSo.DamageableAttackData.AttackParticleType);
        }
        
        protected virtual void SetAttackParticlePosition()
        {
            AttackParticle.transform.position = DamageableView.CurrentTarget.Transform.position;
        }
        
        private void SetActiveAttackParticle()
        {
            AttackParticle.gameObject.SetActive(true);
        }
       
        protected void SetDisabledAttackParticle()
        {
            AttackParticle.gameObject.SetActive(false);
        }
    }
}