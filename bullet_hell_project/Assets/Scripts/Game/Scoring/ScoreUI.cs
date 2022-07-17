using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    public TextMeshProUGUI scoreUi;
    public float bonusScoreDuration = 1.5f;
    public int chainThreshold = 3;

    //
    float bonusScoreCounter;
    int currentScore;
    int bonusScore = 0;
    int bonusScoreFactor = 50;
    int currentChainIndex = 0;
    PolaritySystem.Polarity currentPolarity;
    //

    private void Start()
    {
        currentScore = 0;
        currentPolarity = PolaritySystem.Polarity.none;
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


    public void AddScore(int score, PolaritySystem.Polarity polarity)
    {
        currentScore += score;

        if (Time.time < bonusScoreCounter &&
            currentPolarity == polarity &&
            currentChainIndex < chainThreshold)
        {
            currentScore += bonusScore;
            bonusScore += bonusScoreFactor;
        }
        else
        {
            currentChainIndex = 0;
            currentPolarity = polarity;
            bonusScore = 0;
        }
        bonusScoreCounter = Time.time + bonusScoreDuration;
    }
}
