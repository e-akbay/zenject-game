using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Level
{
    public class LevelMover : ITickable, ILevelMover
    {
        private List<Transform> _moveableObjects = new();
        private float _speed;
        private bool _running;
        
        public void SetSpeed(float speed)
        {
            _speed = speed;
        }

        public void StartMovement()
        {
            _running = true;
        }

        public void StopMovement()
        {
            _running = false;
        }

        public void AddMoveableObject(Transform moveable)
        {
            _moveableObjects.Add(moveable);
        }

        public void RemoveMoveableObject(Transform moveable)
        {
            if(!_moveableObjects.Contains(moveable)) return;
            
            _moveableObjects.Remove(moveable);
        }

        public void Tick()
        {
            if(!_running) return;
            for (int i = 0; i < _moveableObjects.Count; i++)
            {
                _moveableObjects[i].Translate(Vector3.back * (_speed * Time.deltaTime));
            }
        }
    }
}