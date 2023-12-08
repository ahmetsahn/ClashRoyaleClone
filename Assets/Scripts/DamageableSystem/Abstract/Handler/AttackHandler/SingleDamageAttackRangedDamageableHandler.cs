using DG.Tweening;

namespace DamageableSystem.Abstract.Handler.AttackHandler
{
    public abstract class SingleDamageAttackRangedDamageableHandler : DamageableRangedAttackHandler
    {
        protected override void ThrowProjectile()
        {
            base.ThrowProjectile();
            
            ProjectileView.transform.DOMove(TargetPosition, ProjectileArrivalTime)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    ProjectilePoolSignals.OnReturnProjectileToPool(ProjectileView);
                    ControlAndDamageTheTarget();
                });
        }
    }
}