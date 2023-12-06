using System;
using Enums;

namespace Data.ValueType.Damageable
{
    [Serializable]
    public struct RangedDamageableAttackData
    {
        public ProjectileType ProjectileType;
        
        public float ProjectileSpeed;
    }
}