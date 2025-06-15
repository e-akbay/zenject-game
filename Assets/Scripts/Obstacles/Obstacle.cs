using System;
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(PlayerLayerName))
            {
                Debug.Log("Obstacle trigger");
            }
        }

        private void Update()
        {
            if (transform.position.z <= -11f)
            {
                _pool.Despawn(this);
                _levelMover.RemoveMoveableObject(this.transform);
            }
        }

        public class Pool : MonoMemoryPool<Obstacle>
        {
            protected override void Reinitialize(Obstacle obstacle)
            {
                obstacle._type = ObstacleType.Wall;
            }
        }
    }
}