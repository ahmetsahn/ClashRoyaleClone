using System;
using Enums;
using UnityEngine;

namespace Data.ValueType.Damageable
{
    [Serializable]
    public struct DamageableAttackData
    {
        public ParticleType AttackParticleType;
        
        public float AttackDamage;
        public float AttackRange;
        public float AttackSpeed;
        
        public AudioClip AttackSound;
    }
}