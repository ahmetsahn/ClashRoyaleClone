using DamageableSystem.TowerSystem.Handler.MovementHandler;
using DamageableSystem.TowerSystem.Handler.PhysicHandler;
using DamageableSystem.TowerSystem.StateMachine.States;
using Zenject;

namespace DamageableSystem.TowerSystem.Installer.Abstract
{
    public abstract class TowerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<TowerRotationHandler>().AsSingle();
            Container.BindInterfacesTo<TowerTargetDetectionHandler>().AsSingle();
            Container.Bind<TowerIdleState>().AsSingle();
            Container.Bind<TowerAttackState>().AsSingle();
        }
    }
}