using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelMover : MonoBehaviour, ILevelMover
    {
        [SerializeField] private List<Transform> moveableObjects = new List<Transform>();
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
            moveableObjects.Add(moveable);
        }

        private void Update()
        {
            if(!_running) return;
            for (int i = 0; i < moveableObjects.Count; i++)
            {
                moveableObjects[i].Translate(Vector3.back * (_speed * Time.deltaTime));
            }
            
        }
    }
}