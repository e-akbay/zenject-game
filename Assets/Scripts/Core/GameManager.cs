using DI.Signals;
using Zenject;

namespace Core
{
    public class GameManager : IGameManager
    {
        [Inject] SignalBus _signalBus;

        public void StartGame()
        {
            _signalBus.Fire<GameStartedSignal>();
        }

        public void EndGame()
        {
            _signalBus.Fire<GameOverSignal>();
        }

        public void RestartGame()
        {
            _signalBus.Fire<GameRestartedSignal>();
        }
    }
}