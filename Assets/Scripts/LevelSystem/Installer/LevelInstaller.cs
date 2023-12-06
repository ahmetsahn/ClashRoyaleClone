using LevelSystem.Factory;
using LevelSystem.Handler;
using LevelSystem.View;
using Zenject;

namespace LevelSystem.Installer
{
    public class LevelInstaller : Installer<LevelInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindFactory<LevelView, LevelView.Factory>().FromFactory<LevelFactory>();
            
            Container.BindInterfacesTo<LevelHandler>().AsSingle().NonLazy();
        }
    }
}