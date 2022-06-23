using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorrowedTimeCast : MonoBehaviour
{
    TimeJuiceUI timeJuiceUi;
    Unit unit;
    public Transform borrowedTimeShield;
    public float drainDelay = 0.25f;
    float drainCounter;
    bool isShielded = false;
    void Start()
    {
        timeJuiceUi = FindObjectOfType<TimeJuiceUI>();
        unit = GetComponent<Unit>();
        
    }

    void Update()
    {

        if (isShielded && !timeJuiceUi.isJuiceEmpty())
        {
            if (Time.time > drainCounter)
            {
                timeJuiceUi.OnConsumeTimeJuice();
                drainCounter = Time.time + drainDelay;
            }
        }
        else 
        {
            isShielded = false;
            unit.isDestructible = true;
            borrowedTimeShield.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isShielded = true;
            unit.isDestructible = false;
            borrowedTimeShield.gameObject.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isShielded = false;
            unit.isDestructible = true;
            borrowedTimeShield.gameObject.SetActive(false);
        }
    }


}
