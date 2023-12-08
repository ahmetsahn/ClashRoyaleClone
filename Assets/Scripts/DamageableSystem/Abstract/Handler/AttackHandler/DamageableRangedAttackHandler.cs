using Extension;
using ProjectileSystem.View;
using Signal;
using UnityEngine;
using Zenject;

namespace DamageableSystem.Abstract.Handler.AttackHandler
{
    public abstract class DamageableRangedAttackHandler : DamageableBaseAttackHandler
    {
        [field: SerializeField]
        protected Transform ProjectileSpawnPoint { get; private set; }

        protected ProjectilePoolSignals ProjectilePoolSignals;
        
        protected Vector3 TargetPosition;
        
        protected ProjectileView ProjectileView;
        
        protected float ProjectileArrivalTime;
        
        [Inject]
        private void Construct(
            ProjectilePoolSignals projectilePoolSignals)
        {
            ProjectilePoolSignals = projectilePoolSignals;
        }

        protected virtual void ThrowProjectile()
        {
            if(DamageableView.CurrentTarget == null)
            {
                return;
            }
            
            SetProjectile();
            SetProjectilePosition();
            SetActiveProjectile();
            SetTargetPosition();
            SetProjectileArrivalTime();
        }

        private void SetProjectile()
        {
            ProjectileView = ProjectilePoolSignals.OnGetProjectile(DamageableView.DamageableSo.RangedDamageableAttackData.ProjectileType);
        }
        
        private void SetProjectilePosition()
        {
            ProjectileView.transform.MatchPositionToSpawnPoint(ProjectileSpawnPoint);
        }
        
        private void SetActiveProjectile()
        {
            ProjectileView.gameObject.SetActive(true);
        }
        
        private void SetTargetPosition()
        {
            TargetPosition = DamageableView.CurrentTarget.Transform.position;
        }
        
        protected override void SetAttackParticlePosition()
        {
            AttackParticle.transform.position = TargetPosition;
        }
        
        private void SetProjectileArrivalTime()
        {
            ProjectileArrivalTime = Vector3.Distance(ProjectileView.transform.position, TargetPosition) / DamageableView.DamageableSo.RangedDamageableAttackData.ProjectileSpeed;
        }
    }
}