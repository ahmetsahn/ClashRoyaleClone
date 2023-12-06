using System;
using System.Collections.Generic;
using Enums;
using ProjectileSystem.View;
using Signal;
using UnityEngine;

namespace PoolSystem
{
    public class ProjectilePool : IDisposable
    {
        private readonly ProjectileView.Factory _projectileFactory;
        
        private readonly ProjectilePoolSignals _projectilePoolSignals;
        
        private readonly List<GameObject> _objectPool = new();
        
        public ProjectilePool(
            ProjectileView.Factory projectileFactory, 
            ProjectilePoolSignals projectilePoolSignals)
        {
            _projectileFactory = projectileFactory;
            _projectilePoolSignals = projectilePoolSignals;
        
            SubscribeEvents();
        }
    
        private void SubscribeEvents()
        {
            _projectilePoolSignals.OnGetProjectile += OnGetProjectile;
            _projectilePoolSignals.OnReturnProjectileToPool += OnReturnProjectileToPool;
        }
    
        private GameObject CreateProjectile(ProjectileType projectileType)
        {
            var newObject = _projectileFactory.Create(projectileType).gameObject;
            newObject.name = projectileType.ToString();
            return newObject;
        }

        private GameObject OnGetProjectile(ProjectileType projectileType)
        {
            foreach (var projectile in _objectPool)
            {
                if (projectile.activeSelf == false && projectile.name == projectileType.ToString())
                {
                    projectile.SetActive(true);
                    return projectile;
                }
            }
        
            var newProjectile = CreateProjectile(projectileType);
            _objectPool.Add(newProjectile);
            return newProjectile;
        }

        public void OnReturnProjectileToPool(GameObject projectileView)
        {
            projectileView.SetActive(false);
        }
    
        private void UnsubscribeEvents()
        {
            _projectilePoolSignals.OnGetProjectile -= OnGetProjectile;
            _projectilePoolSignals.OnReturnProjectileToPool -= OnReturnProjectileToPool;
        }

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}