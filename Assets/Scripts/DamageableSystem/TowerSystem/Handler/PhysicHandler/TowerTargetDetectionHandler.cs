using DamageableSystem.Abstract.Handler.PhysicHandler;
using DamageableSystem.TowerSystem.View;

namespace DamageableSystem.TowerSystem.Handler.PhysicHandler
{
    public class TowerTargetDetectionHandler : DamageableTargetDetectionHandler
    {
        public TowerTargetDetectionHandler(
            TowerView towerView)
        {
            DamageableView = towerView;
        }
    }
}