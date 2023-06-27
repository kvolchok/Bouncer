using System;
using UnityEngine;

public class Cube : GameEntity
{
    [SerializeField]
    private float _forceMultiplier = 5;
    
    private Action<Cube> _onCubeMoveOutOfMap;
    private Action<int> _onCubePushed;
    private Rigidbody _rigidbody;
    private Camera _camera;

    public void Initialize(Action<Cube> onCubeMoveOutOfMap, Action<int> onCubePushed)
    {
        _onCubeMoveOutOfMap = onCubeMoveOutOfMap;
        _onCubePushed = onCubePushed;
        
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
            _onCubeMoveOutOfMap?.Invoke(this);

            Destroy(gameObject);
        }
    }
    
    private void PushCubeTo(Vector3 point)
    {
        var force = (point - _rigidbody.position) * _forceMultiplier;
        _rigidbody.AddForce(force);
        
        _onCubePushed?.Invoke(1);
    }
}