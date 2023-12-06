using DamageableSystem.Abstract.StateMachine;
using DamageableSystem.CardSystem.View;
using Enums;
using Interfaces;
using Zenject;

namespace DamageableSystem.CardSystem.StateMachine.States
{
    public class TroopCardMovementState : DamageableBaseState,IInitializable
    {
        private readonly TroopCardView _troopCardView;
        
        public TroopCardMovementState(
            TroopCardView troopCardView)
        {
            _troopCardView = troopCardView;
        }
        
        public void Initialize()
        {
            InitializeSettings();
        }
        
        private void InitializeSettings()
        {
            _troopCardView.NavMeshAgent.speed = _troopCardView.DamageableSo.CardMovementData.MovementSpeed;
            _troopCardView.NavMeshAgent.radius = _troopCardView.DamageableSo.CardNavmeshData.NavmeshRadius;
            _troopCardView.NavMeshAgent.height = _troopCardView.DamageableSo.CardNavmeshData.NavmeshHeight;
        }

        public override void EnterState()
        {
            _troopCardView.NavMeshAgent.enabled = true;
            _troopCardView.OnPlayAnimation?.Invoke(TroopCardAnimationType.Movement);
        }

        public override void UpdateState()
        {
            if (_troopCardView.CurrentTarget == null)
            {
                return;
            }
            
            _troopCardView.OnSetNewTarget?.Invoke();
            SetDestination(_troopCardView.CurrentTarget);
        }
        
        public override void ExitState()
        {
            _troopCardView.NavMeshAgent.enabled = false;
        }

        private void SetDestination(IDamageable target)
        {
            _troopCardView.NavMeshAgent.SetDestination(target.Transform.position);
        }
    }
}