using DamageableSystem.TowerSystem.Installer.Abstract;
using DamageableSystem.TowerSystem.StateMachine.Manager;
using DamageableSystem.TowerSystem.StateMachine.States;

namespace DamageableSystem.TowerSystem.Installer
{
    public class AttackTowerInstaller : TowerInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.Bind<AttackTowerDeathState>().AsSingle();
            
            Container.BindInterfacesTo<AttackTowerStateManager>().AsSingle();
        }
    }
}