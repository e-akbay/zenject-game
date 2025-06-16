using UnityEngine;

namespace UI
{
    public class GameScreenBase : MonoBehaviour
    {
        [SerializeField] private GameObject content;
        
        public void Show()
        {
            content.SetActive(true);
        }

        public void Hide()
        {
            content.SetActive(false);
        }
    }
}