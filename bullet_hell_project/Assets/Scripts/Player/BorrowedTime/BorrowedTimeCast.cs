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

    void Start()
    {
        timeJuiceUi = FindObjectOfType<TimeJuiceUI>();
        unit = GetComponent<Unit>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            
        }

        if (Input.GetKey(KeyCode.LeftShift) && Time.time > drainCounter)
        {
            timeJuiceUi.OnConsumeTimeJuice();
            drainCounter = Time.time + drainDelay;
            if (!timeJuiceUi.isJuiceEmpty())
            {
                unit.isDestructible = false;
                borrowedTimeShield.gameObject.SetActive(true);
            }
            else
            { 
                borrowedTimeShield.gameObject.SetActive(false);
                unit.isDestructible = true;
            }
        }
        
        /*
        if (Input.GetKeyUp(KeyCode.LeftShift))
        { 
            borrowedTimeShield.gameObject.SetActive(false);
            unit.isDestructible = true;
        }
        */
    }
}
