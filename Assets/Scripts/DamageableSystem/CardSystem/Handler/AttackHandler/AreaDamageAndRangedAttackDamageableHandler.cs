using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.CardSystem.View;
using DG.Tweening;
using Interfaces;
using UnityEngine;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.AttackHandler
{
    public class AreaDamageAndRangedAttackDamageableHandler : DamageableRangedAttackHandler
    {
        [Inject]
        private void Construct(
            TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
        }

        protected override void ThrowProjectile()
        {
            base.ThrowProjectile();
            
            ProjectileView.transform.DOMove(TargetPosition, ProjectileArrivalTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                ProjectilePoolSignals.OnReturnProjectileToPool(ProjectileView);
                PlayAttackParticle();
                
                Collider[] colliders = Physics.OverlapSphere(ProjectileView.transform.position, DamageableView.DamageableSo.AreaDamageAttackData.DamageRadius);
                foreach (Collider collider in colliders)
                {
                    if (!collider.TryGetComponent(out IDamageable target))
                    {
                        continue;
                    }
                    
                    if (target.DamageableSide == DamageableView.DamageableSide)
                    {
                        continue;
                    }
                    
                    target.TakeDamage(DamageableView.DamageableSo.DamageableAttackData.AttackDamage);
                }
                
                ControlAndFireOnTargetDestroyed();
            });
        }
    }
}