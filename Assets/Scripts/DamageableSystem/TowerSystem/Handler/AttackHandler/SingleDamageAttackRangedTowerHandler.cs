using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.TowerSystem.View;
using Signal;
using Zenject;

namespace DamageableSystem.TowerSystem.Handler.AttackHandler
{
    public class SingleDamageAttackRangedTowerHandler : SingleDamageAttackRangedDamageableHandler
    {
        private TowerView TowerView => (TowerView) DamageableView;
        
        [Inject]
        public void Construct(
            TowerView towerView)
        {
            DamageableView = towerView;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            TowerView.OnEnterAttackState += OnEnterAttackState;
            TowerView.OnExitAttackState += OnExitAttackState;
        }
        
        private void OnEnterAttackState()
        {
            InvokeRepeating(nameof(ThrowProjectile), TowerView.DamageableSo.DamageableRotationData.RotationDuration, 10 / TowerView.DamageableSo.DamageableAttackData.AttackSpeed);
        }
        
        private void OnExitAttackState()
        {
            CancelInvoke(nameof(ThrowProjectile));
        }
        
        private void UnsubscribeEvents()
        {
            TowerView.OnEnterAttackState -= OnEnterAttackState;
            TowerView.OnExitAttackState -= OnExitAttackState;
        }
        
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}