using Level;
using UnityEngine;
using Zenject;

namespace DI.Installers
{
    public class GameInstaller : MonoInstaller<GameInstaller>
    {
        [SerializeField] private LevelMover levelMover;
        [SerializeField] private LevelManager levelManager;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputReader.InputReader>().AsSingle();
            Container.Bind<ILevelMover>().To<LevelMover>().FromInstance(levelMover).AsSingle();
            Container.Bind<ILevelManager>().To<LevelManager>().FromInstance(levelManager).AsSingle();
        }
    }
}