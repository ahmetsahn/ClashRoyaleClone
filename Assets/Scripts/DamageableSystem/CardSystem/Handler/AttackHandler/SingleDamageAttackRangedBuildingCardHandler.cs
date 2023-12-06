using DamageableSystem.Abstract.Handler.AttackHandler;
using DamageableSystem.CardSystem.View;
using Signal;
using Zenject;

namespace DamageableSystem.CardSystem.Handler.AttackHandler
{
    public class SingleDamageAttackRangedBuildingCardHandler : SingleDamageAttackRangedDamageableHandler
    {
        [Inject]
        private void Construct(
            BuildingCardView buildingCardView,
            DamageableSignals damageableSignals,
            ProjectilePoolSignals projectilePoolSignals,
            ParticlePoolSignals particlePoolSignals)
        {
            DamageableView = buildingCardView;
            DamageableSignals = damageableSignals;
            ProjectilePoolSignals = projectilePoolSignals;
            ParticlePoolSignals = particlePoolSignals;
        }
    }
}