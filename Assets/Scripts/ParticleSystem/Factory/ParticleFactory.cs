using System.Collections.Generic;
using Enums;
using ParticleSystem.View;
using UnityEngine;
using Zenject;

namespace ParticleSystem.Factory
{
    public class ParticleFactory : IFactory<ParticleType, ParticleView>
    {
        private readonly IInstantiator _instantiator;
        
        private const string PARTICLE_PREFAB_PATH = "Prefab/Particle/";
        
        private readonly Dictionary<ParticleType, GameObject> _particlePrefabDictionary = new()
        {
            { ParticleType.ArcherQueenAttackParticle, Resources.Load<GameObject>(PARTICLE_PREFAB_PATH + ParticleType.ArcherQueenAttackParticle)},
            { ParticleType.InfernoTowerAttackParticle, Resources.Load<GameObject>(PARTICLE_PREFAB_PATH + ParticleType.InfernoTowerAttackParticle)},
            { ParticleType.WizardAttackParticle, Resources.Load<GameObject>(PARTICLE_PREFAB_PATH + ParticleType.WizardAttackParticle)}
        };
        
        public ParticleFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }
        public ParticleView Create(ParticleType particleType)
        {
            return _instantiator.InstantiatePrefabForComponent<ParticleView>(_particlePrefabDictionary[particleType]);
        }
    }
}