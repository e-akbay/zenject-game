using Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class GameEndScreen : GameScreenBase
    {
        [SerializeField] private Button restartButton;
        [Inject] private IGameManager _gameManager;
        
        private void Start()
        {
            restartButton.onClick.AddListener(RestartGame);
        }

        private void OnDestroy()
        {
            restartButton.onClick.RemoveAllListeners();
        }

        private void RestartGame()
        {
            _gameManager.RestartGame();
        }
    }
}