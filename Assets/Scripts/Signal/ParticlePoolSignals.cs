using System;
using Enums;
using UnityEngine;
using UnityEngine.Events;

namespace Signal
{
    public class ParticlePoolSignals
    {
        public Func<ParticleType, GameObject> OnGetParticle;
        
        public UnityAction<GameObject> OnReturnParticleToPool;
    }
}