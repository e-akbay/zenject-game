using Level;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Obstacles
{
    public class ObstacleSpawner : IObstacleSpawner , ITickable
    {
        private float _spawnRate;
        private float _spawnZ;
        private Vector2 _spawnXMinMax;
        private float _counter;
        
        [Inject] private Obstacle.Pool _obstaclePool;
        [Inject] private ILevelMover _levelMover;
        
        public void SpawnObstacle()
        {
            var obstacle = _obstaclePool.Spawn();
            obstacle.transform.position = new Vector3(Random.Range(_spawnXMinMax.x, _spawnXMinMax.y), 0.5f, _spawnZ);
            _levelMover.AddMoveableObject(obstacle.transform);
        }

        public void SetSpawnRate(float spawnRate)
        {
            _spawnRate = spawnRate;
        }

        public void SetSpawnPositionParameters(float z, Vector2 xMinMax)
        {
            _spawnZ = z;
            _spawnXMinMax = xMinMax;
        }

        public void ClearObstacles()
        {
            _obstaclePool.ClearObstacles();
        }

        public void Tick()
        {
            if(_spawnRate <= 0f) return; // not spawning 
            
            //spawn obstacles
            _counter += Time.deltaTime;
            if (_counter >= _spawnRate)
            {
                _counter = 0f;
                SpawnObstacle();
            }
            
        }
    }
}