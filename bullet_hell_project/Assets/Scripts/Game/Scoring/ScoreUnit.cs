using UnityEngine;

[RequireComponent(typeof(Unit))]
public class ScoreUnit : MonoBehaviour
{
    Unit unit;
    ScoreUI scoreUI;
    public int score = 50;

    void Start()
    {
        unit = GetComponent<Unit>();
        unit.OnDeath += IncreaseScore;
        scoreUI = FindObjectOfType<ScoreUI>();
    }


    void IncreaseScore()
    {
        scoreUI.AddScore(score);
    }
}
