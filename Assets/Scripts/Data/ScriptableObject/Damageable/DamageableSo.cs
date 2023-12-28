using Data.ValueType.Card;
using Data.ValueType.Damageable;
using NaughtyAttributes;
using UnityEngine;

namespace Data.ScriptableObject.Damageable
{
    [CreateAssetMenu(fileName = "DamageableSo", menuName = "Scriptable Objects/DamageableSo", order = 0)]
    public class DamageableSo : UnityEngine.ScriptableObject
    {
        [SerializeField]
        private bool isCard;
        
        [SerializeField]
        private bool isTroopCard;
        
        [SerializeField]
        private bool isBuildingCard;
        
        [SerializeField]
        private bool isRangedAttack;
        
        [SerializeField]
        private bool isAreaDamageAttack;

        [SerializeField] 
        private bool isRotatable;
        
        public DamageableHealthData DamageableHealthData;
        
        public DamageablePhysicData DamageablePhysicData;
        
        public DamageableAttackData DamageableAttackData;
        
        [ShowIf(nameof(isCard))]
        public CardInstallationData CardInstallationData;
        
        [ShowIf(nameof(isTroopCard))]
        public CardMovementData CardMovementData;
        
        [ShowIf(nameof(isTroopCard))]
        public CardNavmeshData CardNavmeshData;
        
        [ShowIf(nameof(isBuildingCard))]
        public BuildingCardLifeTime BuildingCardLifeTime;
        
        [ShowIf(nameof(isRangedAttack))]
        public RangedDamageableAttackData RangedDamageableAttackData;
        
        [ShowIf(nameof(isAreaDamageAttack))]
        public AreaDamageAttackData AreaDamageAttackData;
        
        [ShowIf(nameof(isRotatable))]
        public DamageableRotationData DamageableRotationData;
    }
}