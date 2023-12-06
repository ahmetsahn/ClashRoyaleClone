using DamageableSystem.Abstract.Handler.PhysicHandler;
using DamageableSystem.TowerSystem.View;
using Enums;
using Interfaces;
using UnityEngine;
using Zenject;

namespace DamageableSystem.TowerSystem.Handler.PhysicHandler
{
    public class TowerAttackDetectionHandler : DamageableAttackDetectionHandler
    {
        private TowerView TowerView => (TowerView) DamageableView;
        
        [Inject]
        public void Construct(
            TowerView towerView)
        {
            DamageableView = towerView;
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
            
            TowerView.CurrentTarget = target;
            
            TowerView.OnSwitchState?.Invoke(TowerStateType.Attack);
        }
    }
}