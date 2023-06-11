using UnityEngine;
using UnityEngine.Events;

public class Cylinder : GameEntity
{
    public UnityEvent<Color, int> QuantityChangedEvent;

    private void Start()
    {
        QuantityChangedEvent?.Invoke(Color, 1);
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
            QuantityChangedEvent?.Invoke(Color, -1);

            Destroy(gameObject);
        }
    }
}