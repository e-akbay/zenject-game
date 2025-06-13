using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 5f;
    [SerializeField] private float swerveSpeed = 5f;
    [SerializeField] public float maxX;

    private float _dragX;
    
    [Inject]
    private InputReader.InputReader _input;
    
    private void Update()
    {
        //always forward
        transform.Translate(Vector3.forward * (forwardSpeed * Time.deltaTime));

        if(_input == null) return;
        
        _dragX = _input.dragX;
        
        var position = transform.position;
        position.x = Mathf.Clamp(position.x + _dragX * swerveSpeed, -maxX, maxX);
        transform.position = position;
    }
}