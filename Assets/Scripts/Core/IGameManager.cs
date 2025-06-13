namespace Core
{
    public interface IGameManager
    {
        public void Initialize();
        public void StartGame();
        public void EndGame();
        public void RestartGame();
    }
}