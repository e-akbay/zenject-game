using InputReader;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public float maxX;
    
        private bool _movementStarted;
        private Vector3 _startPosition;
    
        [Inject]
        private IInputReader _input;
    
        private void Update()
        {
            if(_input == null) return;

            if (_input.IsClicking && !_movementStarted)
            {
                _startPosition = transform.position;
                _movementStarted = true;
                return;
            }

            if (!_input.IsClicking && _movementStarted)
            {
                _movementStarted = false;
                return;
            }

            var newPos = _startPosition;
            newPos.x += _input.XOffset;
        
            newPos.x = Mathf.Clamp(newPos.x, -maxX, maxX);
            transform.position = new Vector3(newPos.x, transform.position.y, transform.position.z);
        }
    }
}