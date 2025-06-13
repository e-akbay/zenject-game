using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelManager : MonoBehaviour, ILevelManager
    {
        [SerializeField] private Transform levelRoot;
        [SerializeField] private Transform groundTilePrefab;
        [SerializeField] private int initialGroundTileCount = 4;
        [SerializeField] private float groundTileLength = 10f;
        [SerializeField] private float deadZoneZ;
        
        [Inject]
        private ILevelMover _levelMover;
        
        private Queue<Transform> _groundTiles = new();
        private Transform _currentGroundTile;

        private void Start()
        {
            //Create tiles
            float lastZPosition = 0f;
            for (int i = 0; i < initialGroundTileCount; i++)
            {
                var tile = Instantiate(groundTilePrefab, levelRoot);
                tile.name = "GroundTile" + i;
                tile.transform.position = new Vector3(0f, 0f, lastZPosition);
                _groundTiles.Enqueue(tile);
                
                lastZPosition += groundTileLength;
                _levelMover.AddMoveableObject(tile);
            }
            
            _levelMover.SetSpeed(5f);
            _levelMover.StartMovement();
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
    }
}