using Enums;
using UnityEngine;
using Zenject;

namespace ProjectileSystem.View
{
    public class ProjectileView : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<ProjectileType , ProjectileView> { }
    }
}