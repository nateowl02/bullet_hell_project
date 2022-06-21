using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeJuiceUI : MonoBehaviour
{

    public Image timeJuiceFill;
    public TextMeshProUGUI txtJuiceCount;

    public int[] juicePerLevel;
    int currentJuiceLevel;
    float currentFillAmount;

    private void Start()
    {
        currentJuiceLevel = 0;
        currentFillAmount = 0;
    }

    private void Update()
    {
        timeJuiceFill.fillAmount = currentFillAmount / juicePerLevel[currentJuiceLevel];
        txtJuiceCount.text = currentJuiceLevel < juicePerLevel.Length - 1 ? "x" + currentJuiceLevel.ToString()  : "MAX";
    }

    public void OnCollectTimeJuice()
    {
        if (currentFillAmount == juicePerLevel[currentJuiceLevel] && currentJuiceLevel == juicePerLevel.Length - 1) return;
        currentFillAmount++;
        if (currentFillAmount == juicePerLevel[currentJuiceLevel] &&
            currentJuiceLevel < juicePerLevel.Length - 1)
        {
            currentFillAmount = 0;
            currentJuiceLevel++;
        }
    }

    public void OnConsumeTimeJuice() 
    {
        if (currentFillAmount == 0 && currentJuiceLevel == 0) return;
        currentFillAmount--;
        if (currentFillAmount == 0 && currentJuiceLevel > 0)
        {
            currentJuiceLevel--;
            currentFillAmount = juicePerLevel[currentJuiceLevel];
        }
    }
}
