using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Unit))]
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
    Unit unit;
    float pause;
    //

    void Start()
    {
        unit = GetComponent<Unit>();
        StartCoroutine("MoveToLocation");
    }

    IEnumerator MoveToLocation()
    {
        yield return new WaitForSeconds(delay);
    }
}
