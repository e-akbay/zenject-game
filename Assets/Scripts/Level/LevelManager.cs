using System;
using System.Collections.Generic;
using DI.Signals;
using Obstacles;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelManager : MonoBehaviour, ILevelManager, IInitializable, IDisposable
    {
        [SerializeField] private float deadZoneZ;
        public float DeadZoneZ => deadZoneZ;

        [SerializeField] private Transform levelRoot;
        [SerializeField] private Transform groundTilePrefab;
        [SerializeField] private int initialGroundTileCount = 4;
        [SerializeField] private float groundTileLength = 10f;
        [SerializeField] private float obstacleSpawnZ;
        [SerializeField] private Vector2 obstacleSpawnXMinMax;

        [SerializeField] private float levelSpeed = 5f;
        [SerializeField] private float obstaclePoolSpawnRate = 2f;
        
        [Inject] private ILevelMover _levelMover;
        [Inject] private IObstacleSpawner _obstacleSpawner;
        [Inject] private SignalBus _signalBus;
        
        private Queue<Transform> _groundTiles = new();
        private Transform _currentGroundTile;
        
        private const string GroundTileName = "Ground";

        private void Start()
        {
            //Create tiles
            float lastZPosition = 0f;
            for (int i = 0; i < initialGroundTileCount; i++)
            {
                var tile = Instantiate(groundTilePrefab, levelRoot);
                tile.name = GroundTileName + i;
                tile.transform.position = new Vector3(0f, 0f, lastZPosition);
                _groundTiles.Enqueue(tile);
                
                lastZPosition += groundTileLength;
                _levelMover.AddMoveableObject(tile);
            }
            _currentGroundTile = _groundTiles.Dequeue();
        }

        private void Update()
        {
            if (_currentGroundTile.position.z < deadZoneZ)
            {
                UpdateLevel();
            }
        }

        public void UpdateLevel()
        {
            _currentGroundTile.position +=  Vector3.forward * (groundTileLength * initialGroundTileCount);
            _groundTiles.Enqueue(_currentGroundTile);
            _currentGroundTile = _groundTiles.Dequeue();
        }

        private void StartLevel()
        {
            _levelMover.SetSpeed(levelSpeed);
            _levelMover.StartMovement();
            
            _obstacleSpawner.SetSpawnRate(obstaclePoolSpawnRate);
            _obstacleSpawner.SetSpawnPositionParameters(obstacleSpawnZ, obstacleSpawnXMinMax);
        }

        private void StopLevel()
        {
            _levelMover.StopMovement();
            _obstacleSpawner.SetSpawnRate(0f);
            _obstacleSpawner.ClearObstacles();
        }

        public void Initialize()
        {
            _signalBus.Subscribe<GameStartedSignal>(StartLevel);
            _signalBus.Subscribe<GameOverSignal>(StopLevel);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartedSignal>(StartLevel);
            _signalBus.Unsubscribe<GameOverSignal>(StopLevel);
        }
    }
}