using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private readonly List<Counter> _cylinderCounters = new();
    
    [SerializeField]
    private Counter _cylinderCounterPrefab;
    [SerializeField]
    private Counter _cubeMovementCounterPrefab;
    
    private Counter _cubeMovementCounter;

    public void CreateCylinderCounters(ColorsProvider colorsProvider)
    {
        var colors = colorsProvider.GetAllColors();

        foreach (var color in colors)
        {
            var cylinderCounter = Instantiate(_cylinderCounterPrefab, transform);
            cylinderCounter.SetColor(color);
            _cylinderCounters.Add(cylinderCounter);
        }
    }

    public void CreateCubeMovementCounter()
    {
        _cubeMovementCounter = Instantiate(_cubeMovementCounterPrefab, transform);
    }

    public void ChangeCylinderCount(Color color, int value)
    {
        foreach (var counter in _cylinderCounters)
        {
            if (counter.Color == color)
            {
                counter.ChangeScore(value);
            }
        }
    }

    public void ChangeCubeMovementCount(int value)
    {
        _cubeMovementCounter.ChangeScore(value);
    }
}