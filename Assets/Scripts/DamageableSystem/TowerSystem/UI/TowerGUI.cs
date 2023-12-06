using DamageableSystem.Abstract.UI;
using DamageableSystem.TowerSystem.View;
using Zenject;

namespace DamageableSystem.TowerSystem.UI
{
    public class TowerGUI : DamageableGUI
    {
        [Inject]
        public void Construct(
            TowerView towerView)
        {
            DamageableView = towerView;
        }
    }
}