using UnityEngine;

public class PolaritySystem : MonoBehaviour
{

    public Transform hope;
    public Transform despair;

    public enum Polarity { 
        hope,
        despair,
        none
    }

    public Polarity currentPolarity = Polarity.hope;
    public static float polarityDamageFactor = 1.5f;

    public void SwitchPolarity()
    {
        currentPolarity = currentPolarity == Polarity.hope ? Polarity.despair : Polarity.hope;
        hope.gameObject.SetActive(currentPolarity == Polarity.hope);
        despair.gameObject.SetActive(currentPolarity == Polarity.despair);
    }

}
