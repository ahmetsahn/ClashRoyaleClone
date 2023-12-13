using DamageableSystem.CardSystem.Factory;
using DamageableSystem.CardSystem.View.Abstract;
using Data.ScriptableObject;
using Enums;
using InputSystem;
using LevelSystem.Installer;
using ParticleSystem.Factory;
using ParticleSystem.View;
using PoolSystem;
using ProjectileSystem.Factory;
using ProjectileSystem.View;
using Signal;
using SpawnerSystem;
using UISystem;
using UISystem.Manager;
using UnityEngine;
using Zenject;

namespace Core.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField]
        private AudioClip leftClickSound;
        
        [SerializeField]
        private AudioClip rightClickSound;
        
        [SerializeField] 
        private Transform[] layers;
        
        [SerializeField]
        private SpawnerSo spawnerSo;
        
        public override void InstallBindings()
        {
            Container.BindFactory<CardType, CardView, CardView.Factory>().FromFactory<CardFactory>();
            Container.BindFactory<ProjectileType, ProjectileView, ProjectileView.Factory>().FromFactory<ProjectileFactory>();
            Container.BindFactory<ParticleType, ParticleView, ParticleView.Factory>().FromFactory<ParticleFactory>();
            
            Container.BindInterfacesTo<UIPanelHandler>().AsSingle().WithArguments(layers);
            Container.BindInterfacesTo<UIManager>().AsSingle();
            Container.BindInterfacesTo<InputHandler>().AsSingle().WithArguments(leftClickSound, rightClickSound);
            Container.BindInterfacesTo<CardSpawner>().AsSingle();
            
            Container.Bind<CardPool>().AsSingle().NonLazy();
            Container.Bind<ProjectilePool>().AsSingle().NonLazy();
            Container.Bind<ParticlePool>().AsSingle().NonLazy();
            
            LevelInstaller.Install(Container);
            
            SignalBindings();
        }
        
        private void SignalBindings()
        {
            Container.Bind<CoreGameSignals>().AsSingle();
            Container.Bind<CardPoolSignals>().AsSingle();
            Container.Bind<UISignals>().AsSingle();
            Container.Bind<LevelSignals>().AsSingle();
            Container.Bind<ButtonSignals>().AsSingle();
            Container.Bind<InputSignals>().AsSingle();
            Container.Bind<DamageableSignals>().AsSingle();
            Container.Bind<ProjectilePoolSignals>().AsSingle();
            Container.Bind<ParticlePoolSignals>().AsSingle();
        }
    }
}