using DamageableSystem.CardSystem.Handler.PhysicHandler;
using DamageableSystem.CardSystem.StateMachine.Managers;
using DamageableSystem.CardSystem.StateMachine.States;
using Zenject;

namespace DamageableSystem.CardSystem.Installer
{
    public class BuildingCardInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<BuildingCardIdleState>().AsSingle();
            Container.Bind<BuildingCardAttackState>().AsSingle();
            Container.Bind<BuildingCardDeathState>().AsSingle();
            
            Container.BindInterfacesTo<BuildingCardTargetDetectionHandler>().AsSingle();
            Container.BindInterfacesTo<BuildingCardStateManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<BuildingCardInitializationState>().AsSingle();
        }
    }
}