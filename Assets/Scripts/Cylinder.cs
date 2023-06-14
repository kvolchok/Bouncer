using System;
using UnityEngine;

public class Cylinder : GameEntity
{
    private Action<Color, int> _quantityChangedEvent;

    public void Initialize(Action<Color, int> quantityChangedEvent)
    {
        _quantityChangedEvent = quantityChangedEvent;
        
        _quantityChangedEvent?.Invoke(Color, 1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag(GlobalConstants.CUBE_TAG))
        {
            return;
        }
        
        var cube = collision.gameObject.GetComponent<Cube>();
        if (cube.Color == Color)
        {
            _quantityChangedEvent?.Invoke(Color, -1);

            Destroy(gameObject);
        }
    }
}