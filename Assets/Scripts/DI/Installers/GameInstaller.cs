using Core;
using DI.Signals;
using InputReader;
using Level;
using Obstacles;
using UI;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private Obstacle obstaclePrefab;

        [SerializeField] private GameEndScreen gameOverScreen;
        [SerializeField] private GameStartScreen startScreen;
        [SerializeField] private InGameScreen gameScreen;
        
        public override void InstallBindings()
        {
            BindManagers();
            BindUI();
            BindSignals();
        }

        private void BindManagers()
        {
            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle();

#if UNITY_EDITOR
            
            Container.BindInterfacesAndSelfTo<MouseInputReader>().AsSingle();
#elif UNITY_ANDROID || UNITY_IOS
            Container.BindInterfacesAndSelfTo<TouchInputReader>().AsSingle();
#endif
            
            Container.BindInterfacesAndSelfTo<LevelMover>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelManager>().FromInstance(levelManager).AsSingle();

            Container.BindMemoryPool<Obstacle, Obstacle.Pool>().WithInitialSize(20)
                .FromComponentInNewPrefab(obstaclePrefab).UnderTransformGroup("Obstacles");

            Container.BindInterfacesAndSelfTo<ObstacleSpawner>().AsSingle();
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<UIManager>().AsSingle();
            
            Container.Bind<GameEndScreen>().FromInstance(gameOverScreen).AsSingle();
            Container.Bind<GameStartScreen>().FromInstance(startScreen).AsSingle();
            Container.Bind<InGameScreen>().FromInstance(gameScreen).AsSingle();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<GameOverSignal>();
            Container.DeclareSignal<GameStartedSignal>();
            Container.DeclareSignal<GameRestartedSignal>();
        }
    }
}