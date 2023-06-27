using System;
using UnityEngine;

public class Sphere : GameEntity
{
    private Action _onSphereDestroy;

    public void Initialize(Action onSphereDestroy)
    {
        _onSphereDestroy = onSphereDestroy;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag(GlobalConstants.CYLINDER_TAG))
        {
            _onSphereDestroy?.Invoke();
            
            Destroy(gameObject);
            return;
        }

        if (otherCollider.CompareTag(GlobalConstants.CUBE_TAG))
        {
            var cube = otherCollider.gameObject.GetComponent<Cube>();
            cube.SetColor(Color);
            _onSphereDestroy?.Invoke();

            Destroy(gameObject);
        }
    }
}