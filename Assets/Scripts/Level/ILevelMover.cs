using UnityEngine;

namespace Level
{
    public interface ILevelMover
    {
        public void SetSpeed(float speed);
        public void StartMovement();
        public void StopMovement();
        public void AddMoveableObject(Transform moveable);
    }
}