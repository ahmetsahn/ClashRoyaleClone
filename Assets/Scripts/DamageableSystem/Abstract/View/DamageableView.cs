using System;
using Data.ScriptableObject.Damageable;
using Enums;
using Interfaces;
using Signal;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace DamageableSystem.Abstract.View
{
    public abstract class DamageableView : MonoBehaviour, IDamageable
    {
        [field: SerializeField]
        public DamageableSideType DamageableSide { get; set; }
        
        [field: SerializeField]
        public DamageableSo DamageableSo { get; private set; }
        
        public Transform Transform => transform;
        
        public IDamageable CurrentTarget;
        
        private DamageableSignals _damageableSignals;
        
        public UnityAction OnSetNewTarget;
        
        public UnityAction<bool> OnSetAttackColliderEnabled;
        
        public UnityAction<float> OnTakeDamage;
        
        public Func<bool> IsTargetInAttackRange;
        
        public bool IsDead { get; set; }
        
        private float _currentHealth;
        
        [Inject]
        private void Construct(
            DamageableSignals damageableSignals)
        {
            _damageableSignals = damageableSignals;
        }

        private void OnEnable()
        {
            SubscribeEvents();
            InitializeDamageable();
        }
        
        protected virtual void SubscribeEvents()
        {
            _damageableSignals.OnTargetDestroyed += OnTargetDestroyed;
        }
        
        protected virtual void InitializeDamageable()
        {
            IsDead = false;
            _currentHealth = DamageableSo.DamageableHealthData.MaxHealth;
        }

        protected abstract void OnTargetDestroyed(IDamageable target);

        public virtual void TakeDamage(float damage)
        {
            _currentHealth -= damage;
            OnTakeDamage?.Invoke(damage);

            if (!(_currentHealth <= 0))
            {
                return;
            }
            
            IsDead = true;
        }
        
        protected virtual void UnsubscribeEvents()
        {
            _damageableSignals.OnTargetDestroyed -= OnTargetDestroyed;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}