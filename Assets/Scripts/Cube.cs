using System;
using UnityEngine;

public class Cube : GameEntity
{
    [SerializeField]
    private float _forceMultiplier = 5;
    
    private Action<Cube> _outOfMapEvent;
    private Action<int> _cubePushedEvent;
    private Rigidbody _rigidbody;
    private Camera _camera;

    public void Initialize(Action<Cube> outOfMapEvent, Action<int> cubePushedEvent)
    {
        _outOfMapEvent = outOfMapEvent;
        _cubePushedEvent = cubePushedEvent;
        
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
            PushCubeTo(hitInfo.point);
        }
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag(GlobalConstants.OUT_OF_MAP_TAG))
        {
            _outOfMapEvent?.Invoke(this);

            Destroy(gameObject);
        }
    }
    
    private void PushCubeTo(Vector3 point)
    {
        var force = (point - _rigidbody.position) * _forceMultiplier;
        _rigidbody.AddForce(force);
        
        _cubePushedEvent?.Invoke(1);
    }
}