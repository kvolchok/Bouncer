using UnityEngine;
using UnityEngine.Events;

public class Sphere : GameEntity
{
    public UnityEvent SphereDestroyEvent;

    private void OnTriggerEnter(Collider otherCollider)
    {
        if (otherCollider.CompareTag(GlobalConstants.CYLINDER_TAG))
        {
            SphereDestroyEvent?.Invoke();
            
            Destroy(gameObject);
            return;
        }

        if (otherCollider.CompareTag(GlobalConstants.CUBE_TAG))
        {
            var cube = otherCollider.gameObject.GetComponent<Cube>();
            cube.SetColor(Color);
            SphereDestroyEvent?.Invoke();

            Destroy(gameObject);
        }
    }
}