using DamageableSystem.CardSystem.UI.Abstract;
using DamageableSystem.CardSystem.View;
using Zenject;

namespace DamageableSystem.CardSystem.UI
{
    public class TroopCardGUI : CardGUI
    {
        [Inject]
        private void Construct(
            TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
        }
    }
}