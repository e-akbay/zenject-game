using System;
using DI.Signals;
using Zenject;

namespace UI
{
    public class UIManager : IUIManager, IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;

        [Inject] private GameEndScreen _gameOverScreen;
        [Inject] private GameStartScreen _startScreen;
        [Inject] private InGameScreen _gameScreen;
        
        
        public void ShowGameOver()
        {
            _startScreen.Hide();
            _gameScreen.Hide();
            _gameOverScreen.Show();
        }

        public void ShowStart()
        {
            _startScreen.Show();
            _gameScreen.Hide();
            _gameOverScreen.Hide();
        }

        public void ShowInGame()
        {
            _startScreen.Hide();
            _gameScreen.Show();
            _gameOverScreen.Hide();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(ShowInGame);
            _signalBus.Subscribe<GameRestartedSignal>(ShowStart);
            _signalBus.Subscribe<GameOverSignal>(ShowGameOver);
            
            ShowStart();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(ShowInGame);
            _signalBus.Unsubscribe<GameRestartedSignal>(ShowStart);
            _signalBus.Unsubscribe<GameOverSignal>(ShowGameOver);
        }
    }
}