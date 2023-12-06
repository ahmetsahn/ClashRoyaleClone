using DamageableSystem.Abstract.View;
using Interfaces;
using UnityEngine;

namespace DamageableSystem.Abstract.Handler.PhysicHandler
{
    public abstract class DamageableAttackDetectionHandler : MonoBehaviour
    {
        protected DamageableView DamageableView;
        
        [SerializeField]
        private SphereCollider attackDetectionCollider;

        private void Awake()
        {
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            attackDetectionCollider.radius = DamageableView.DamageableSo.DamageableAttackData.AttackRange;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            DamageableView.IsTargetInAttackRange += IsTargetInAttackRange;
            DamageableView.OnSetAttackColliderEnabled += OnSetAttackColliderEnabled;
        }

        protected abstract void OnTriggerEnter(Collider other);

        private bool IsTargetInAttackRange()
        {
            var targetColliders = Physics.OverlapSphere(DamageableView.Transform.position, DamageableView.DamageableSo.DamageableAttackData.AttackRange);
            
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

                if (target == DamageableView.CurrentTarget)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        private void OnSetAttackColliderEnabled(bool isEnabled)
        {
            attackDetectionCollider.enabled = isEnabled;
        }
        
        private void UnsubscribeEvents()
        {
            DamageableView.IsTargetInAttackRange -= IsTargetInAttackRange;
            DamageableView.OnSetAttackColliderEnabled -= OnSetAttackColliderEnabled;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}