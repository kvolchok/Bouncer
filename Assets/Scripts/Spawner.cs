using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private int _randomRadius = 5;

    public GameEntity Spawn(ColorsProvider colorsProvider, GameEntity prefab)
    {
        var gameEntity = Instantiate(prefab, transform);

        if (gameEntity is Cube or Counter)
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
        gameEntity.transform.localPosition = gameEntityPosition;
    }
}