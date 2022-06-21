using UnityEngine;
using System;

public class TimeJuiceCollector : MonoBehaviour
{
    public string collectionTag = "TimeJuice";

    TimeJuiceUI timeJuiceUi;

    private void Start()
    {
        timeJuiceUi = FindObjectOfType<TimeJuiceUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == collectionTag)
        {
            timeJuiceUi.OnCollectTimeJuice();
            Destroy(other.gameObject);
        }
    }
}
