using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorrowedTimeCast : MonoBehaviour
{
    TimeJuiceUI timeJuiceUi;
    public float drainDelay = 0.25f;
    float drainCounter;

    void Start()
    {
        timeJuiceUi = FindObjectOfType<TimeJuiceUI>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Time.time > drainCounter) 
        {
            timeJuiceUi.OnConsumeTimeJuice();
            drainCounter = Time.time + drainDelay;
        }
    }
}
