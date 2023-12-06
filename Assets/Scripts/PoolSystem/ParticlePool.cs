using System;
using System.Collections.Generic;
using Enums;
using ParticleSystem.View;
using Signal;
using UnityEngine;

namespace PoolSystem
{
    public class ParticlePool : IDisposable
    {
        private readonly ParticleView.Factory _particleFactory;
        
        private readonly ParticlePoolSignals _particlePoolSignals;
        
        private readonly List<GameObject> _objectPool = new();
        
        public ParticlePool(
            ParticleView.Factory particleFactory, 
            ParticlePoolSignals particlePoolSignals)
        {
            _particleFactory = particleFactory;
            _particlePoolSignals = particlePoolSignals;
        
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _particlePoolSignals.OnGetParticle += OnGetParticle;
            _particlePoolSignals.OnReturnParticleToPool += OnReturnParticleToPool;
        }
        
        private GameObject CreateParticle(ParticleType particleType)
        {
            var newObject = _particleFactory.Create(particleType).gameObject;
            newObject.name = particleType.ToString();
            return newObject;
        }
        
        public GameObject OnGetParticle(ParticleType particleType)
        {
            foreach (var particle in _objectPool)
            {
                if (particle.activeSelf == false && particle.name == particleType.ToString())
                {
                    particle.SetActive(true);
                    return particle;
                }
            }
            
            var newParticle = CreateParticle(particleType);
            _objectPool.Add(newParticle);
            return newParticle;
        }
        
        public void OnReturnParticleToPool(GameObject particleView)
        {
            particleView.SetActive(false);
        }
        
        private void UnsubscribeEvents()
        {
            _particlePoolSignals.OnGetParticle += OnGetParticle;
            _particlePoolSignals.OnReturnParticleToPool += OnReturnParticleToPool;
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}