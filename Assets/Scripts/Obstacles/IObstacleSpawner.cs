using UnityEngine;

namespace Obstacles
{
    public interface IObstacleSpawner
    {
        public void SpawnObstacle();
        public void SetSpawnRate(float spawnRate);
        public void SetSpawnPositionParameters(float z, Vector2 xMinMax);
    }
}