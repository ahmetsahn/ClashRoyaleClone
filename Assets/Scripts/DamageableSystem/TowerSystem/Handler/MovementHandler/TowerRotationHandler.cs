using DamageableSystem.Abstract.Handler.MovementHandler;
using DamageableSystem.TowerSystem.View;
using DG.Tweening;
using UnityEngine;

namespace DamageableSystem.TowerSystem.Handler.MovementHandler
{
    public class TowerRotationHandler : DamageableRotationHandler
    {
        private TowerView TowerView => (TowerView) DamageableView;
        
        public TowerRotationHandler(
            TowerView towerView)
        {
            DamageableView = towerView;
        }

        protected override void SetTransform()
        {
            Transform = TowerView.TowerDefenderTransform;
        }

        protected override void SubscribeEvents()
        {
            TowerView.OnUpdateAttackState += OnRotateToTarget;
            TowerView.OnExitAttackState += OnResetRotation;
        }
        
        private void OnResetRotation()
        {
            TowerView.TowerDefenderTransform.DORotateQuaternion(
                Quaternion.Euler(0f, TowerView.DamageableSide == Enums.DamageableSideType.Friendly ? 180f : 0f, 0f), 
                TowerView.DamageableSo.DamageableRotationData.RotationDuration);
        }

        protected override void UnsubscribeEvents()
        {
            TowerView.OnUpdateAttackState -= OnRotateToTarget;
            TowerView.OnExitAttackState -= OnResetRotation;
        }
    }
}

