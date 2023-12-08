using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.CardSystem.View;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.AttackHandler
{
    public class SingleDamageAttackRangedTroopCardHandler : SingleDamageAttackRangedDamageableHandler
    {
        [Inject]
        private void Construct(
            TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
        }
    }
}