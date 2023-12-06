using DamageableSystem.CardSystem.UI.Abstract;
using DamageableSystem.CardSystem.View;
using Zenject;

namespace DamageableSystem.CardSystem.UI
{
    public class BuildingCardGUI : CardGUI
    {
        [Inject]
        private void Construct(
            BuildingCardView buildingCardView)
        {
            DamageableView = buildingCardView;
        }
    }
}