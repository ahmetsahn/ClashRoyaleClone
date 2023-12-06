using System.Collections.Generic;
using Enums;
using ProjectileSystem.View;
using UnityEngine;
using Zenject;

namespace ProjectileSystem.Factory
{
    public class ProjectileFactory : IFactory<ProjectileType, ProjectileView>
    {
        private readonly IInstantiator _instantiator;
        
        private const string PROJECTILE_PREFAB_PATH = "Prefab/Projectile/";
        
        private readonly Dictionary<ProjectileType, GameObject> _cardTypeCardPrefabDictionary = new()
        {
            { ProjectileType.ArcherProjectile, Resources.Load<GameObject>(PROJECTILE_PREFAB_PATH + ProjectileType.ArcherProjectile)},
            { ProjectileType.SparkyProjectile, Resources.Load<GameObject>(PROJECTILE_PREFAB_PATH + ProjectileType.SparkyProjectile)},
            { ProjectileType.WizardProjectile, Resources.Load<GameObject>(PROJECTILE_PREFAB_PATH + ProjectileType.WizardProjectile)},
            { ProjectileType.GolemProjectile, Resources.Load<GameObject>(PROJECTILE_PREFAB_PATH + ProjectileType.GolemProjectile)}
        };
        
        public ProjectileFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        
        public ProjectileView Create(ProjectileType projectileType)
        {
            var projectile = _instantiator.InstantiatePrefabForComponent<ProjectileView>(_cardTypeCardPrefabDictionary[projectileType]);
            return projectile;
        }
    }
}