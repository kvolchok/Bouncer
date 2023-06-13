using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameEntity _cylinderPrefab;
    [SerializeField]
    private int _cylindersCount = 6;
    [SerializeField]
    private GameEntity _spherePrefab;
    [SerializeField]
    private GameEntity _cubePrefab;

    [SerializeField]
    private Spawner _spawner;
    [SerializeField]
    private ColorsProvider _colorsProvider;
    [SerializeField]
    private UIController _uiController;
    
    private Sphere _sphere;
    private Cube _cube;

    private Color _cubeColor;

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
                cylinder.QuantityChangedEvent.AddListener(_uiController.ChangeCylinderCount);
            }
        
            if (gameEntity is Sphere sphere)
            {
                _sphere = sphere;
                _sphere.SphereDestroyEvent.AddListener(() => CreateGameEntity(_spherePrefab));
            }

            if (gameEntity is Cube cube)
            {
                _cube = cube;
                _cube.OutOfMapEvent.AddListener(SaveCurrentPlayerColor);
                _cube.OutOfMapEvent.AddListener(() => CreateGameEntity(_cubePrefab));
                _cube.OutOfMapEvent.AddListener(() => _cube.SetColor(_cubeColor));
                _cube.CubePushedEvent.AddListener(_uiController.ChangeCubeMovementCount);
            }
        }
    }

    private void SaveCurrentPlayerColor()
    {
        _cubeColor = _cube.Color;
    }

    private void OnDestroy()
    {
        _sphere.SphereDestroyEvent.RemoveAllListeners();
        _cube.OutOfMapEvent.RemoveAllListeners();
        _cube.CubePushedEvent.RemoveAllListeners();
    }
}