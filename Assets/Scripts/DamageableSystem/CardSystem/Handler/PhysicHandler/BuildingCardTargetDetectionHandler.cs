using DamageableSystem.Abstract.Handler.PhysicHandler;
using DamageableSystem.CardSystem.View;

namespace DamageableSystem.CardSystem.Handler.PhysicHandler
{
    public class BuildingCardTargetDetectionHandler : DamageableTargetDetectionHandler
    {
        public BuildingCardTargetDetectionHandler(
            BuildingCardView buildingCardView)
        {
            DamageableView = buildingCardView;
        }
    }
}