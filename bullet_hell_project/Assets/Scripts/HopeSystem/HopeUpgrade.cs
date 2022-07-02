using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopeUpgrade : MonoBehaviour
{
    public GameObject[] hopeUpgrades;
    int currentHopeLevel;
    HopeUI hopeSystem;

    private void Start()
    {
        hopeSystem = FindObjectOfType<HopeUI>();
    }

    private void Update()
    {
        int hopeSystemCurrentLevel = hopeSystem.GetCurrentLevel(hopeUpgrades.Length);
       
        if (currentHopeLevel != hopeSystemCurrentLevel)
        {
            currentHopeLevel = hopeSystemCurrentLevel;
            SetWeaponIndex(currentHopeLevel);
        }
    }

    public void SetWeaponIndex(int index)
    {
        for (int i = 0; i < hopeUpgrades.Length; i++)
        {
            hopeUpgrades[i].SetActive(index == i ? true : false);
        }
    }

}
