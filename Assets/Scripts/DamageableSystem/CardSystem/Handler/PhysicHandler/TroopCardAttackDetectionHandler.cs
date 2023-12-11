using DamageableSystem.Abstract.Handler.PhysicHandler;
using DamageableSystem.CardSystem.View;
using Enums;
using Interfaces;
using UnityEngine;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.PhysicHandler
{
    public class TroopCardAttackDetectionHandler : DamageableAttackDetectionHandler
    {
        private TroopCardView TroopCardView => (TroopCardView) DamageableView;
        
        [Inject]
        private void Construct(
            TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
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
            
            TroopCardView.OnSetNewTarget?.Invoke();
            
            TroopCardView.OnSwitchState?.Invoke(TroopCardStateType.Attack);
        }
    }
}