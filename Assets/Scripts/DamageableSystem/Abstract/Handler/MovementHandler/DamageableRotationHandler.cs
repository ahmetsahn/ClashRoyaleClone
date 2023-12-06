using System;
using DamageableSystem.Abstract.View;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace DamageableSystem.Abstract.Handler.MovementHandler
{
    public abstract class DamageableRotationHandler : IInitializable, IDisposable
    {
        protected DamageableView DamageableView;

        protected Transform Transform;
        
        public void Initialize()
        {
            SubscribeEvents();
            SetTransform();
        }
        
        protected abstract void SubscribeEvents();
        
        protected abstract void SetTransform();
        
        protected void OnRotateToTarget()
        {
            var targetPosition = DamageableView.CurrentTarget.Transform.position;
            var targetXZPosition = new Vector3(targetPosition.x, Transform.position.y, targetPosition.z);
            Transform.DOLookAt(targetXZPosition, DamageableView.DamageableSo.DamageableRotationData.RotationDuration);
        }
        
        protected abstract void UnsubscribeEvents();

        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}