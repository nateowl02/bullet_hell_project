using System.Collections;
using UnityEngine;

[RequireComponent(typeof(UnitOld))]
public class UnitBehavior : MonoBehaviour
{
    public enum BehaviorTypes
    {
        None,
        Intercepting,
        Descending,
        Drifting
    }
    public BehaviorTypes behavior;
    public float delay;
    public bool isEscaping = false;
    //
    UnitOld unit;
    float pause;
    //

    void Start()
    {
        unit = GetComponent<UnitOld>();
        StartCoroutine("MoveToLocation");
    }

    IEnumerator MoveToLocation()
    {
        yield return new WaitForSeconds(delay);
    }
}
