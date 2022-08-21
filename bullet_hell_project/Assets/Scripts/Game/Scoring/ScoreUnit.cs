using UnityEngine;


public class ScoreUnit : MonoBehaviour
{
    public int score = 50;
    //

    ScoreUI scoreUI;
    
    //

    void Start()
    {
        /*
        unit = GetComponent<UnitOld>();
        unit.OnDeath += IncreaseScore;
        scoreUI = FindObjectOfType<ScoreUI>();
        */
    }

    void IncreaseScore()
    {
        PolaritySystem polarity = GetComponent<PolaritySystem>();
        scoreUI.AddScore(score, polarity.currentPolarity);
    }
}
