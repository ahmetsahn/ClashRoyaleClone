using DamageableSystem.Abstract.Handler.MovementHandler;
using DamageableSystem.CardSystem.Handler.AnimationHandler;
using DamageableSystem.CardSystem.Handler.MovementHandler;
using DamageableSystem.CardSystem.Handler.PhysicHandler;
using DamageableSystem.CardSystem.StateMachine.Managers;
using DamageableSystem.CardSystem.StateMachine.States;
using Zenject;

namespace DamageableSystem.CardSystem.Installer
{
    public class TroopCardInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TroopCardInitializationState>().AsSingle();
            Container.Bind<TroopCardIdleState>().AsSingle();
            Container.Bind<TroopCardAttackState>().AsSingle();
            Container.Bind<TroopCardDeathState>().AsSingle();
            
            Container.BindInterfacesTo<TroopCardTargetDetectionHandler>().AsSingle();
            Container.BindInterfacesTo<TroopCardAnimationHandler>().AsSingle();
            Container.BindInterfacesTo<TroopCardRotationHandler>().AsSingle();
            Container.BindInterfacesTo<TroopCardStateManager>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<TroopCardMovementState>().AsSingle();
        }
    }
}