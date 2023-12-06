using Interfaces;
using UnityEngine.Events;

namespace Signal
{
    public class DamageableSignals
    {
        public UnityAction<IDamageable> OnTargetDestroyed;
    }
}