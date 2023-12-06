using DamageableSystem.TowerSystem.StateMachine.States.Abstract;
using DamageableSystem.TowerSystem.View;

namespace DamageableSystem.TowerSystem.StateMachine.States
{
    public class AttackTowerDeathState : TowerDeathState
    {
        public AttackTowerDeathState(TowerView towerView)
        {
            TowerView = towerView;
        }
    }
}