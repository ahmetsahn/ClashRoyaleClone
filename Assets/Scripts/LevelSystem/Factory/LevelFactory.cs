using LevelSystem.View;
using UnityEngine;
using Zenject;

namespace LevelSystem.Factory
{
    public class LevelFactory : IFactory<LevelView>
    {
        private readonly IInstantiator _instantiator;
        
        private const string LEVEL_PATH = "Prefab/Level/Level";
        
        public LevelFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        public LevelView Create()
        {
            return _instantiator.InstantiatePrefabForComponent<LevelView>(Resources.Load<GameObject>(LEVEL_PATH));
        }
    }
}