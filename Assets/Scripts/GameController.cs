using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Cylinder _cylinderPrefab;
    [SerializeField]
    private int _cylindersCount = 6;
    [SerializeField]
    private Sphere _spherePrefab;
    [SerializeField]
    private Cube _cubePrefab;
    
    [SerializeField]
    private ColorsProvider _colorsProvider;
    [SerializeField]
    private Spawner _spawner;
    [SerializeField]
    private UIController _uiController;

    private void Awake()
    {
        _uiController.CreateCylinderCounters(_colorsProvider);
        _uiController.CreateCubeMovementCounter();
        
        CreateGameEntity(_cylinderPrefab, _cylindersCount);
        CreateGameEntity(_spherePrefab);
        CreateGameEntity(_cubePrefab);
    }

    private void CreateGameEntity(GameEntity prefab, int count = 1)
    {
        for (var i = 0; i < count; i++)
        {
            var gameEntity = _spawner.Spawn(_colorsProvider, prefab);

            if (gameEntity is Cylinder cylinder)
            {
                cylinder.Initialize(_uiController.ChangeCylinderCount);
            }
        
            if (gameEntity is Sphere sphere)
            {
                sphere.Initialize(() => CreateGameEntity(_spherePrefab));
            }

            if (gameEntity is Cube cube)
            {
                cube.Initialize(OnCubeMoveOutOfMap, _uiController.ChangeCubeMovementCount);
            }
        }
    }

    private void OnCubeMoveOutOfMap(Cube cube)
    {
        var color = cube.Color;
        cube = _spawner.Spawn(_colorsProvider, _cubePrefab);
        cube.Initialize(OnCubeMoveOutOfMap, _uiController.ChangeCubeMovementCount);
        cube.SetColor(color);
    }
}