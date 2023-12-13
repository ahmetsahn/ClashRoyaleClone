using System;
using DamageableSystem.TowerSystem.StateMachine.States.Abstract;
using DamageableSystem.TowerSystem.View;
using Enums;
using Signal;

namespace DamageableSystem.TowerSystem.StateMachine.States
{
    public class BossTowerDeathState : TowerDeathState
    {
        private readonly CoreGameSignals _coreGameSignals;
        
        public BossTowerDeathState(
            TowerView towerView, 
            CoreGameSignals coreGameSignals)
        {
            TowerView = towerView;
            _coreGameSignals = coreGameSignals;
        }

        public override void EnterState()
        {
            base.EnterState();
            _coreGameSignals.OnGameEnd?.Invoke();

            switch (TowerView.DamageableSide)
            {
                case DamageableSideType.Friendly:
                    _coreGameSignals.OnLose?.Invoke();
                    break;
                case DamageableSideType.Enemy:
                    _coreGameSignals.OnWin?.Invoke();
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}