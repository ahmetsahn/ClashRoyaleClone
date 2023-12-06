using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.CardSystem.View;
using Signal;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.AttackHandler
{
    public class SingleDamageAttackRangedTroopCardHandler : SingleDamageAttackRangedDamageableHandler
    {
        [Inject]
        private void Construct(
            TroopCardView troopCardView,
            DamageableSignals damageableSignals,
            ProjectilePoolSignals projectilePoolSignals,
            ParticlePoolSignals particlePoolSignals)
        {
            DamageableView = troopCardView;
            DamageableSignals = damageableSignals;
            ProjectilePoolSignals = projectilePoolSignals;
            ParticlePoolSignals = particlePoolSignals;
        }
    }
}