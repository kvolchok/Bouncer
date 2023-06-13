using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class ColorsProvider
{
    [SerializeField]
    private Color[] _colors;

    public Color GetRandomColor()
    {
        var index = Random.Range(0, _colors.Length);
        return _colors[index];
    }

    public Color[] GetAllColors()
    {
        return _colors;
    }
}