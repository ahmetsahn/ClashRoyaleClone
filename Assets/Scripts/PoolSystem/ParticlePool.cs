using System;
using System.Collections.Generic;
using Enums;
using ParticleSystem.View;
using Signal;

namespace PoolSystem
{
    public class ParticlePool : IDisposable
    {
        private readonly ParticleView.Factory _particleFactory;
        
        private readonly ParticlePoolSignals _particlePoolSignals;
        
        private readonly List<ParticleView> _objectPool = new();
        
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
        
        private ParticleView CreateParticle(ParticleType particleType)
        {
            ParticleView newObject = _particleFactory.Create(particleType);
            newObject.name = particleType.ToString();
            return newObject;
        }
        
        private ParticleView OnGetParticle(ParticleType particleType)
        {
            foreach (ParticleView particle in _objectPool)
            {
                if (particle.gameObject.activeSelf == false && particle.name == particleType.ToString())
                {
                    particle.gameObject.SetActive(true);
                    return particle;
                }
            }
            
            ParticleView newParticle = CreateParticle(particleType);
            _objectPool.Add(newParticle);
            return newParticle;
        }
        
        private void OnReturnParticleToPool(ParticleView particleView)
        {
            particleView.gameObject.SetActive(false);
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