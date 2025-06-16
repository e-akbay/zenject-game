using UnityEngine;
using Zenject;

namespace InputReader
{
    public class TouchInputReader : IInitializable, ITickable, IInputReader
    {
        public bool IsClicking => _isClicking;
        public float XOffset => _xOffset;
        
        private Camera _cam;
        private Vector3 _touchOrigin;
        
        private bool _isClicking;
        private float _xOffset;
        
        public void Initialize()
        {
            _cam = Camera.main;
        }

        public void Tick()
        {
            if(Input.touchCount <= 0)
                return;
            
            var touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Began)
            {
                _isClicking = true;
                Vector3 touchPos = touch.position;
                _touchOrigin = _cam.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, _cam.transform.position.z));
            }
            
            if (touch.phase == TouchPhase.Ended ||
                touch.phase == TouchPhase.Canceled)
            {
                _isClicking = false;
            }
        
            // Calculate movement while touching
            if (_isClicking)
            {
                Vector3 touchPos = touch.position;
                Vector3 currentTouchPos = _cam.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, _cam.transform.position.z));
            
                // Calculate offset from origin
                _xOffset = currentTouchPos.x - _touchOrigin.x;
            
                // Reverse X
                _xOffset = -_xOffset;
            }
        }
    }
}