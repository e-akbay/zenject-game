using Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class GameStartScreen : GameScreenBase
    {
        [SerializeField] private Button startButton;
        [Inject] private IGameManager _gameManager;
        
        private void Start()
        {
            startButton.onClick.AddListener(RestartGame);
        }

        private void OnDestroy()
        {
            startButton.onClick.RemoveAllListeners();
        }

        private void RestartGame()
        {
            _gameManager.StartGame();
        }
    }
}