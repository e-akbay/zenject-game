using UnityEngine;
using Zenject;

namespace InputReader
{
    public class InputReader : ITickable
    {
        private float _startX;
        public float dragX;
        
        public void Tick()
        {
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0))
            {
                _startX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                dragX = 0;
            }
            else if (Input.GetMouseButton(0))
            {
                float drag = Input.mousePosition.x - _startX;
                float normalized = drag / Screen.width;
                dragX = normalized;
            }
#endif
        }
    }
}