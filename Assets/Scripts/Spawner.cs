using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private int _randomRadius = 5;

    public T Spawn<T>(ColorsProvider colorsProvider, T prefab) where T : GameEntity 
    {
        var gameEntity = Instantiate(prefab, transform);

        if (gameEntity is Cube)
        {
            return gameEntity;
        }

        SetRandomPosition(gameEntity);
        var color = colorsProvider.GetRandomColor();
        gameEntity.SetColor(color);

        return gameEntity;
    }

    private void SetRandomPosition(GameEntity gameEntity)
    {
        var randomPosition = Random.insideUnitCircle * _randomRadius;
        var gameEntityPosition = gameEntity.transform.position;

        gameEntityPosition.x = randomPosition.x;
        gameEntityPosition.z = randomPosition.y;
        gameEntity.transform.position = gameEntityPosition;
    }
}