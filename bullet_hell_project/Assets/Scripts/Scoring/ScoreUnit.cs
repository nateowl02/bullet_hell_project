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
        PolaritySystem polarity = GetComponent<PolaritySystem>();
        scoreUI.AddScore(score, polarity.currentPolarity);
    }
}
