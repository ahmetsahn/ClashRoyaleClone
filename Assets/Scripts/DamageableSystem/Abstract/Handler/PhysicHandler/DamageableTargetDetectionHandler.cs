using System;
using DamageableSystem.Abstract.View;
using Interfaces;
using UnityEngine;
using Zenject;

namespace DamageableSystem.Abstract.Handler.PhysicHandler
{
    public abstract class DamageableTargetDetectionHandler : IInitializable, IDisposable
    {
        protected DamageableView DamageableView;

        public void Initialize()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            DamageableView.OnSetNewTarget += OnSetNewTarget;
        }

        private void OnSetNewTarget()
        {
            var targetColliders = Physics.OverlapSphere(DamageableView.Transform.position, DamageableView.DamageableSo.DamageablePhysicData.TargetDetectionRange);
            var closestDistance = Mathf.Infinity;
            
            DamageableView.CurrentTarget = null;

            foreach (var targetCollider in targetColliders)
            {
                if (!targetCollider.TryGetComponent(out IDamageable target))
                {
                    continue;
                }
                
                if (target.DamageableSide == DamageableView.DamageableSide)
                {
                    continue;
                }
                
                var distanceToTarget = Vector3.Distance(DamageableView.Transform.position, target.Transform.position);

                if (!(distanceToTarget < closestDistance)) continue;
                
                closestDistance = distanceToTarget;
                DamageableView.CurrentTarget = target;
            }
        }
        
        private void UnsubscribeEvents()
        {
            DamageableView.OnSetNewTarget -= OnSetNewTarget;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}