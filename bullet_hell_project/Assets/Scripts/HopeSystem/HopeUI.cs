using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class HopeUI : MonoBehaviour
{

    public Image hopeMeter;
    public Image despairMeter;
    public TextMeshProUGUI hopeValue;
    public TextMeshProUGUI despairValue;
    public float maxHope = 150;
    [Range(0, 150)]
    public float currentHope;

    float fillAmount;

    void Start()
    {
        currentHope = maxHope / 2;
    }


    void Update()
    {
        fillAmount = currentHope / maxHope;
        hopeMeter.fillAmount = fillAmount;
        despairMeter.fillAmount = 1 - fillAmount;

        hopeValue.text = (Mathf.Round( hopeMeter.fillAmount * 100)).ToString() + "%";
        despairValue.text = (Mathf.Round( despairMeter.fillAmount * 100)).ToString() + "%";
    }

    public void OnAbsorb(PolaritySystem.Polarity polarity)
    {
        switch (polarity)
        {
            case PolaritySystem.Polarity.hope:
                currentHope = currentHope + 1 > maxHope ? currentHope : currentHope + 1;
                break;
            case PolaritySystem.Polarity.despair:
                currentHope = currentHope - 1 < 0 ? currentHope : currentHope - 1;
                break;
            default:
                break;
        }

    }

    public int GetCurrentLevel(int numberOfUpgrades)
    {
        return (int) Mathf.Round((currentHope / maxHope) * numberOfUpgrades) - 1;
    }

}
