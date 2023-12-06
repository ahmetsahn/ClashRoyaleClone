using DamageableSystem.Abstract.Handler.PhysicHandler;
using DamageableSystem.CardSystem.View;
using Enums;
using Interfaces;
using UnityEngine;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.PhysicHandler
{
    public class BuildingCardAttackDetectionHandler : DamageableAttackDetectionHandler
    {
        private BuildingCardView BuildingCardView => (BuildingCardView) DamageableView;
        
        [Inject]
        private void Construct(
            BuildingCardView buildingCardView)
        {
            DamageableView = buildingCardView;
        }
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable target))
            {
                return;
            }

            if (target.DamageableSide == DamageableView.DamageableSide)
            {
                return;
            }
            
            BuildingCardView.CurrentTarget = target;
            
            BuildingCardView.OnSwitchState?.Invoke(BuildingCardStateType.Attack);
        }
    }
}