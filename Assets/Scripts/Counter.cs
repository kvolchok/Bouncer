using TMPro;
using UnityEngine;

public class Counter : GameEntity
{
    [SerializeField]
    private TextMeshProUGUI _score;

    private int _value;

    private void Start()
    {
        SetScore();
    }

    public void ChangeScore(int value)
    {
        _value += value;
        SetScore();
    }

    private void SetScore()
    {
        _score.text = _value.ToString();
    }
}