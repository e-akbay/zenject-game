using System;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        public GameState currentState = GameState.Warmup;
        
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            //load level etc etc
        }

        public void StartGame()
        {
            currentState = GameState.Playing;
        }

        public void EndGame()
        {
            currentState = GameState.Over;
        }

        public void RestartGame()
        {
            currentState = GameState.Warmup;
        }
    }
}