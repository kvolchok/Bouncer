using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private readonly List<Counter> _cylinderCounters = new();
    
    [SerializeField]
    private Counter _cylinderCounterPrefab;
    [SerializeField]
    private int _cylinderCountersNumber = 3;
    [SerializeField]
    private Counter _cubeMovementCounterPrefab;
    
    private Counter _cubeMovementCounter;

    public void CreateCylinderCounters(Spawner spawner, ColorsProvider colorsProvider)
    {
        for (var i = 0; i < _cylinderCountersNumber; i++)
        {
            var cylinderCounter = spawner.Spawn(colorsProvider, _cylinderCounterPrefab) as Counter;
            cylinderCounter.transform.SetParent(transform);
            var color = colorsProvider.GetColorByIndex(i);
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