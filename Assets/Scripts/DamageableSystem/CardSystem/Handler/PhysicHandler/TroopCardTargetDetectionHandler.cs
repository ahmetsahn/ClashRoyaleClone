using DamageableSystem.Abstract.Handler.PhysicHandler;
using DamageableSystem.CardSystem.View;

namespace DamageableSystem.CardSystem.Handler.PhysicHandler
{
    public class TroopCardTargetDetectionHandler : DamageableTargetDetectionHandler
    {
        public TroopCardTargetDetectionHandler(
            TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
        }
    }
}