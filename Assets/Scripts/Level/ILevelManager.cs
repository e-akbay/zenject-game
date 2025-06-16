namespace Level
{
    public interface ILevelManager
    {
        public float DeadZoneZ { get; }
        public void UpdateLevel();
    }
}