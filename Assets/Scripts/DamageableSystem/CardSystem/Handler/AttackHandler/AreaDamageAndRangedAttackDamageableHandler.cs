using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.CardSystem.View;
using DG.Tweening;
using Interfaces;
using Signal;
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
            
            Projectile.transform.DOMove(TargetPosition, ProjectileArrivalTime).SetEase(Ease.Linear).OnComplete(() =>
            {
                ProjectilePoolSignals.OnReturnProjectileToPool(Projectile);
                PlayAttackParticle();
                
                var colliders = Physics.OverlapSphere(Projectile.transform.position, DamageableView.DamageableSo.AreaDamageAttackData.DamageRadius);
                foreach (var collider in colliders)
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