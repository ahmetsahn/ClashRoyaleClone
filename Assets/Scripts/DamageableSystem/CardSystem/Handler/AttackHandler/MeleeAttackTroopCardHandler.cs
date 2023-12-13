using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.CardSystem.View;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.AttackHandler
{
    public class MeleeAttackTroopCardHandler : DamageableBaseAttackHandler
    {
        [Inject]
        private void Construct(
            TroopCardView troopCardView)
        {
            DamageableView = troopCardView;
        }

        protected override void ControlAndDamageTheTarget()
        {
            base.ControlAndDamageTheTarget();
            PlayAttackSound();
        }
    }
}