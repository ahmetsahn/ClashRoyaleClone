using System;
using Enums;
using ProjectileSystem.View;
using UnityEngine;
using UnityEngine.Events;

namespace Signal
{
    public class ProjectilePoolSignals
    {
        public Func<ProjectileType, ProjectileView> OnGetProjectile;
        
        public UnityAction<ProjectileView> OnReturnProjectileToPool; 
    }
}