using System.Collections.Generic;
using Core;
using Level;
using UnityEngine;
using Zenject;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {
        private ObstacleType _type;
        private const string PlayerLayerName = "Player";
        
        [Inject] private Pool _pool;
        [Inject] private ILevelMover _levelMover;
        [Inject] private IGameManager _gameManager;
        [Inject] private ILevelManager _levelManager;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
            {
                _gameManager.EndGame();
            }
        }

        private void Update()
        {
            if (transform.localPosition.z <= _levelManager.DeadZoneZ)
            {
                _pool.Despawn(this);
                _levelMover.RemoveMoveableObject(this.transform);
            }
        }

        public class Pool : MonoMemoryPool<Obstacle>
        {
            private List<Obstacle> _activeObstacles = new List<Obstacle>();
            [Inject] private ILevelMover _levelMover;
            
            protected override void Reinitialize(Obstacle obstacle)
            {
                obstacle._type = ObstacleType.Wall;
            }

            protected override void OnSpawned(Obstacle item)
            {
                base.OnSpawned(item);
                _activeObstacles.Add(item);
            }

            protected override void OnDespawned(Obstacle item)
            {
                base.OnDespawned(item);
                _activeObstacles.Remove(item);
            }

            public void ClearObstacles()
            {
                for (int i = 0; i < _activeObstacles.Count; i++)
                {
                    _levelMover.RemoveMoveableObject(_activeObstacles[i].transform);
                    Despawn(_activeObstacles[i]);
                }
                _activeObstacles.Clear();
            }
        }
    }
}