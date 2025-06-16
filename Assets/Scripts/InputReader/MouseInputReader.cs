using UnityEngine;
using Zenject;

namespace InputReader
{
    public class MouseInputReader : IInitializable, ITickable, IInputReader
    {
        public bool IsClicking => _isClicking;
        public float XOffset => _xOffset;
        
        private Camera _cam;
        private Vector3 _clickOrigin;
        
        private bool _isClicking;
        private float _xOffset;
        
        public void Initialize()
        {
            _cam = Camera.main;
        }
        
        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isClicking = true;
                Vector3 mousePos = Input.mousePosition;
                _clickOrigin = _cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _cam.transform.position.z));
            }
        
            // Check for mouse click release
            if (Input.GetMouseButtonUp(0))
            {
                _isClicking = false;
            }
        
            // Calculate movement while clicking
            if (_isClicking)
            {
                Vector3 mousePos = Input.mousePosition;
                Vector3 currentMousePos = _cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _cam.transform.position.z));
            
                // Calculate offset from click origin
                _xOffset = currentMousePos.x - _clickOrigin.x;
            
                // Reverse X
                _xOffset = -_xOffset;
            }
        }
    }
}