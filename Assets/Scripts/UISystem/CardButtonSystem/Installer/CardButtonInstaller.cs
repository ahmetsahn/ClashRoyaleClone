using UISystem.CardButtonSystem.State;
using Zenject;

namespace UISystem.CardButtonSystem.Installer
{
    public class CardButtonInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<CardButtonStateManager>().AsSingle();
            Container.Bind<CardButtonSelectionState>().AsSingle();
            Container.Bind<CardButtonPlayState>().AsSingle();
        }
    }
}