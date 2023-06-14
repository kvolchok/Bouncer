using System;
using UnityEngine;

public class Sphere : GameEntity
{
    private Action _sphereDestroyEvent;

    public void Initialize(Action sphereDestroyEvent)
    {
        _sphereDestroyEvent = sphereDestroyEvent;
    }

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag(GlobalConstants.CYLINDER_TAG))
        {
            _sphereDestroyEvent?.Invoke();
            
            Destroy(gameObject);
            return;
        }

        if (otherCollider.CompareTag(GlobalConstants.CUBE_TAG))
        {
            var cube = otherCollider.gameObject.GetComponent<Cube>();
            cube.SetColor(Color);
            _sphereDestroyEvent?.Invoke();

            Destroy(gameObject);
        }
    }
}