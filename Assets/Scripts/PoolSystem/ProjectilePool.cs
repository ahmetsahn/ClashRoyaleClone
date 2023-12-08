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
        
        private readonly List<ProjectileView> _objectPool = new();
        
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
    
        private ProjectileView CreateProjectile(ProjectileType projectileType)
        {
            ProjectileView newObject = _projectileFactory.Create(projectileType);
            newObject.name = projectileType.ToString();
            return newObject;
        }

        private ProjectileView OnGetProjectile(ProjectileType projectileType)
        {
            foreach (ProjectileView projectile in _objectPool)
            {
                if (projectile.gameObject.activeSelf == false && projectile.name == projectileType.ToString())
                {
                    projectile.gameObject.SetActive(true);
                    return projectile;
                }
            }
        
            ProjectileView newProjectile = CreateProjectile(projectileType);
            _objectPool.Add(newProjectile);
            return newProjectile;
        }

        private void OnReturnProjectileToPool(ProjectileView projectileView)
        {
            projectileView.gameObject.SetActive(false);
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