using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    public Color Color { get; private set; }
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        Color = _renderer.material.color;
    }

    public void SetColor(Color color)
    {
        _renderer.material.color = color;
        Color = color;
    }
}