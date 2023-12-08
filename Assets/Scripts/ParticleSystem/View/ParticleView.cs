using Enums;
using Signal;
using UnityEngine;
using Zenject;

namespace ParticleSystem.View
{
    public class ParticleView : MonoBehaviour
    {
        [SerializeField]
        private bool isLooping;
        [SerializeField] 
        private UnityEngine.ParticleSystem particleEffect;
        
        private ParticlePoolSignals _particlePoolSignals;
        
        [Inject]
        public void Construct(ParticlePoolSignals particlePoolSignals)
        {
            _particlePoolSignals = particlePoolSignals;
        }

        private void OnEnable()
        {
            if (isLooping)
            {
                return;
            }
            
            Invoke(nameof(ParticleSystemStopped), particleEffect.main.duration);
        }
        
        private void ParticleSystemStopped()
        {
            _particlePoolSignals.OnReturnParticleToPool?.Invoke(this);
        }

        public class Factory : PlaceholderFactory<ParticleType, ParticleView> { }
    }
}