using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreUi;
    public float bonusScoreDuration = 1.5f;

    //
    float bonusScoreCounter;
    int currentScore;
    int bonusScore = 0;
    int bonusScoreFactor = 50;
    //

    private void Start()
    {
        currentScore = 0;
    }

    void Update()
    {
        scoreUi.text = "SCORE:" + currentScore.ToString();
    }

    public void AddScore(int score)
    {
        currentScore += score;

        if (Time.time < bonusScoreCounter)
        {
            currentScore += bonusScore;
            bonusScore += bonusScoreFactor;
        }
        else
            bonusScore = 0;
        bonusScoreCounter = Time.time + bonusScoreDuration;
    }

}
