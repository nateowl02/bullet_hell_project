using UnityEngine;

[RequireComponent(typeof(Unit))]
public class ScoreUnit : MonoBehaviour
{
    public int score = 50;
    //
    Unit unit;
    ScoreUI scoreUI;
    //

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
