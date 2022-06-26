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
        Vector3 targetLocation = EmpireanMath.GetTarget(transform.position, "Player", true);

        while (behavior == BehaviorTypes.Intercepting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, unit.speed * Time.deltaTime);
            if (transform.position ==  targetLocation)
            {
                targetLocation = EmpireanMath.GetTarget(transform.position, "Player", true);
                yield return new WaitForSeconds(delay);
            }
            yield return null;
        }

        while (behavior == BehaviorTypes.Descending)
        {
            if (Time.time < pause || delay == 0)
                transform.Translate((Vector3.down * unit.speed) * Time.deltaTime);
            else
            {
                yield return new WaitForSeconds(delay);
                pause = Time.time + delay;
            }

            yield return null;
        }
     
        targetLocation = EmpireanMath.GetRandomLocation();

        while (behavior == BehaviorTypes.Drifting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, unit.speed * Time.deltaTime);
            if (transform.position == targetLocation)
            {
                targetLocation = EmpireanMath.GetRandomLocation();
                yield return new WaitForSeconds(delay);
            }
            yield return null;
        }

    }
}
