using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.CardSystem.View;
using UnityEngine;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.AttackHandler
{
    public class LaserAttackBuildingCardHandler : DamageableBaseAttackHandler
    {
        [SerializeField]
        private LineRenderer lineRenderer;
        
        [SerializeField]
        private Transform laserSpawnPoint;
        
        private BuildingCardView BuildingCardView => (BuildingCardView) DamageableView;
        
        [Inject]
        private void Construct(
            BuildingCardView buildingCardView)
        {
            DamageableView = buildingCardView;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            BuildingCardView.OnEnterAttackState += OnSetEnabledLineRenderer;
            BuildingCardView.OnEnterAttackState += PlayAttackParticle;
            BuildingCardView.OnUpdateAttackState += OnApplyDamage;
            BuildingCardView.OnFixedUpdateAttackState += OnSetLineRendererPosition;
            BuildingCardView.OnFixedUpdateAttackState += SetAttackParticlePosition;
            BuildingCardView.OnExitAttackState += OnDisabledLineRenderer;
            BuildingCardView.OnExitAttackState += SetDisabledAttackParticle;
        }

        protected override void ControlAndDamageTheTarget()
        {
            if (DamageableView.CurrentTarget == null)
            {
                return;
            }
            DamageableView.CurrentTarget.TakeDamage(DamageableView.DamageableSo.DamageableAttackData.AttackDamage);
            ControlAndFireOnTargetDestroyed();
        }

        private void OnSetEnabledLineRenderer()
        {
            lineRenderer.enabled = true;
            OnSetLineRendererPosition();
        }
        
        private void OnSetLineRendererPosition()
        {
            lineRenderer.SetPosition(0, laserSpawnPoint.position);
            lineRenderer.SetPosition(1, BuildingCardView.CurrentTarget.Transform.position);
        }
        
        private void OnApplyDamage()
        {
            ControlAndDamageTheTarget();
        }
        
        private void OnDisabledLineRenderer()
        {
            lineRenderer.enabled = false;
        }
        
        private void UnsubscribeEvents()
        {
            BuildingCardView.OnEnterAttackState -= OnSetEnabledLineRenderer;
            BuildingCardView.OnEnterAttackState -= PlayAttackParticle;
            BuildingCardView.OnUpdateAttackState -= OnApplyDamage;
            BuildingCardView.OnFixedUpdateAttackState -= OnSetLineRendererPosition;
            BuildingCardView.OnFixedUpdateAttackState -= SetAttackParticlePosition;
            BuildingCardView.OnExitAttackState -= OnDisabledLineRenderer;
            BuildingCardView.OnExitAttackState -= SetDisabledAttackParticle;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}