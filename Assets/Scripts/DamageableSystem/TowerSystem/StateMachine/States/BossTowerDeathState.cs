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
        
        private readonly UISignals _uiSignals;
        
        public BossTowerDeathState(
            TowerView towerView, 
            CoreGameSignals coreGameSignals,
            UISignals uiSignals)
        {
            TowerView = towerView;
            _coreGameSignals = coreGameSignals;
            _uiSignals = uiSignals;
        }

        public override void EnterState()
        {
            base.EnterState();
            _coreGameSignals.OnGameEnd?.Invoke();

            switch (TowerView.DamageableSide)
            {
                case DamageableSideType.Friendly:
                    _uiSignals.OnOpenPanel?.Invoke(UIPanelType.GameOverPanel, 0);
                    break;
                case DamageableSideType.Enemy:
                    _uiSignals.OnOpenPanel?.Invoke(UIPanelType.WinPanel, 0);
                    break;
                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}