using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.CardSystem.View;
using Signal;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.AttackHandler
{
    public class MeleeAttackCardHandler : DamageableBaseAttackHandler
    {
        [Inject]
        private void Construct(
            TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
        }
    }
}