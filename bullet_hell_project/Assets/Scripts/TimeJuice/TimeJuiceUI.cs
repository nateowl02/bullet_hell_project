using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeJuiceUI : MonoBehaviour
{
    public Image timeJuiceFill;
    public TextMeshProUGUI txtJuiceCount;
    public int[] juicePerLevel;
    public int currentJuiceLevel = 0;
    //
    float currentFillAmount;
    //

    private void Start()
    {
        currentJuiceLevel = 0;
        currentFillAmount = 0;
    }

    private void Update()
    {
        timeJuiceFill.fillAmount = currentFillAmount / juicePerLevel[currentJuiceLevel];
        txtJuiceCount.text = (currentFillAmount == juicePerLevel[currentJuiceLevel] && currentJuiceLevel == juicePerLevel.Length - 1) ? "MAX" : "x" + currentJuiceLevel.ToString();
    }

    public void OnCollectTimeJuice()
    {
        if (currentFillAmount == juicePerLevel[currentJuiceLevel] && 
            currentJuiceLevel == juicePerLevel.Length - 1) return;

        currentFillAmount++;

        if (currentFillAmount >= juicePerLevel[currentJuiceLevel] &&
            currentJuiceLevel < juicePerLevel.Length - 1)
        {
            currentFillAmount = 0;
            currentJuiceLevel++;
        }
    }

    public void OnConsumeTimeJuice() 
    {
        if (isJuiceEmpty()) return;

        currentFillAmount--;

        if (currentFillAmount <= 0 && currentJuiceLevel > 0)
        {
            currentJuiceLevel--;
            currentFillAmount = juicePerLevel[currentJuiceLevel];
        }
    }

    public bool isJuiceEmpty()
    {
        return (currentFillAmount == 0 && currentJuiceLevel == 0);
    }
}
