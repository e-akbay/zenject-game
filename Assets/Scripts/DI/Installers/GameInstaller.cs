using Level;
using Obstacles;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private LevelManager levelManager;
        [SerializeField] private Obstacle obstaclePrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputReader.InputReader>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelMover>().AsSingle();
            Container.Bind<ILevelManager>().To<LevelManager>().FromInstance(levelManager).AsSingle();

            Container.BindMemoryPool<Obstacle, Obstacle.Pool>().WithInitialSize(20)
                .FromComponentInNewPrefab(obstaclePrefab).UnderTransformGroup("Obstacles");

            Container.BindInterfacesAndSelfTo<ObstacleSpawner>().AsSingle();
        }
    }
}