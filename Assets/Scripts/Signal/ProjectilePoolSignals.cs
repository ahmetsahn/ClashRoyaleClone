using System;
using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Signal
{
    public class ProjectilePoolSignals
    {
        public Func<ProjectileType, GameObject> OnGetProjectile;
        
        public UnityAction<GameObject> OnReturnProjectileToPool; 
    }
}