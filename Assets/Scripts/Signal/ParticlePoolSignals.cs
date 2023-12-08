using System;
using Enums;
using ParticleSystem.View;
using UnityEngine;
using UnityEngine.Events;

namespace Signal
{
    public class ParticlePoolSignals
    {
        public Func<ParticleType, ParticleView> OnGetParticle;
        
        public UnityAction<ParticleView> OnReturnParticleToPool;
    }
}