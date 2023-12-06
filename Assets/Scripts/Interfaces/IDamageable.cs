using Enums;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageable
    {
        public DamageableSideType DamageableSide { get; }
        public Transform Transform { get; }
        public bool IsDead { get; set; }
        public void TakeDamage(float damage);
    }
}