using DamageableSystem.TowerSystem.Installer.Abstract;
using DamageableSystem.TowerSystem.StateMachine.Manager;
using DamageableSystem.TowerSystem.StateMachine.States;

namespace DamageableSystem.TowerSystem.Installer
{
    public class BossTowerInstaller : TowerInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            Container.Bind<BossTowerDeathState>().AsSingle();
            Container.BindInterfacesTo<BossTowerStateManager>().AsSingle();
        }
    }
}