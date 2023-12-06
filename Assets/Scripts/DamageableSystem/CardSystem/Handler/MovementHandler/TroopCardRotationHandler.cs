using DamageableSystem.Abstract.Handler.MovementHandler;
using DamageableSystem.CardSystem.View;
using Enums;
using UnityEngine;

namespace DamageableSystem.CardSystem.Handler.MovementHandler
{
    public class TroopCardRotationHandler : DamageableRotationHandler
    {
        private TroopCardView TroopCardView => (TroopCardView) DamageableView;
        
        public TroopCardRotationHandler(TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
        }
        
        protected override void SetTransform()
        {
            Transform = TroopCardView.Transform;
        }
        
        protected override void SubscribeEvents()
        {
            TroopCardView.OnRotateToTarget += OnRotateToTarget;
            TroopCardView.OnSetInitialRotation += OnSetInitialRotation;
        }
        
        private void OnSetInitialRotation()
        {
            DamageableView.Transform.rotation = DamageableView.DamageableSide == DamageableSideType.Friendly
                ? Quaternion.Euler(0f, 180f, 0f)
                : Quaternion.Euler(0f, 0f, 0f);
        }

        protected override void UnsubscribeEvents()
        {
            TroopCardView.OnRotateToTarget -= OnRotateToTarget;
            TroopCardView.OnSetInitialRotation -= OnSetInitialRotation;
        }
    }
}