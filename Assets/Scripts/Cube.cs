using UnityEngine;
using UnityEngine.Events;

public class Cube : GameEntity
{
    public UnityEvent OutOfMapEvent;
    public UnityEvent<int> CubePushedEvent;
    
    [SerializeField]
    private float _forceMultiplier = 5;
    
    private Rigidbody _rigidbody;
    private Camera _camera;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Update()
    {
        var mousePosition = Input.mousePosition;
        var ray = _camera.ScreenPointToRay(mousePosition);

        if (!Physics.Raycast(ray, out var hitInfo))
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Push(hitInfo.point);
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag(GlobalConstants.OUT_OF_MAP_TAG))
        {
            OutOfMapEvent?.Invoke();

            Destroy(gameObject);
        }
    }
    
    private void Push(Vector3 point)
    {
        var force = (point - _rigidbody.position) * _forceMultiplier;
        _rigidbody.AddForce(force);
        
        CubePushedEvent?.Invoke(1);
    }
}