using UnityEngine;

public class WeaponUpgrader : MonoBehaviour
{
    public GameObject[] weaponList;
    int currentUpgradeLevel = 0;
    TimeJuiceUI timeJuiceUi;

    private void Start()
    {
        timeJuiceUi = FindObjectOfType<TimeJuiceUI>();
    }

    public void SetWeaponIndex(int index)
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            weaponList[i].SetActive(index == i ? true : false);
        }
    }

    private void Update()
    {

        if (currentUpgradeLevel != timeJuiceUi.currentJuiceLevel)
        {
            currentUpgradeLevel = timeJuiceUi.currentJuiceLevel;
            SetWeaponIndex(currentUpgradeLevel);
        }
    }

}
