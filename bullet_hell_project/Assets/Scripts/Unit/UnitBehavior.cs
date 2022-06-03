using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitBehavior : MonoBehaviour
{

    public enum BehaviorTypes
    {
        None,
        Intercepting,
        Descending,
        Avoiding

    }
    public BehaviorTypes behavior;
    public float delay;
    
    Unit unit;
    Vector2 TargetLocation;
    float pause;

    void Start()
    {
        unit = GetComponent<Unit>();
        StartCoroutine("MoveToLocation");
    }


    Vector3 GetPlayerLocation()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            return new Vector3(Random.Range(-GameRules.screenWidth, GameRules.screenWidth), transform.position.y, 0);
        }

        return new Vector3(player.transform.position.x, Random.Range(GameRules.screenHeight, GameRules.screenHeight / 2), 0);

    }

    Vector2 GetRandomLocation()
    {
        return new Vector2(Random.Range(-GameRules.screenWidth, GameRules.screenWidth), Random.Range(GameRules.screenHeight, GameRules.screenHeight / 2));
    }

    IEnumerator MoveToLocation()
    {

        Vector3 targetLocation = GetPlayerLocation();

        while (behavior == BehaviorTypes.Intercepting)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, unit.speed * Time.deltaTime);

            if (transform.position ==  targetLocation)
            {
                targetLocation = GetPlayerLocation();
                yield return new WaitForSeconds(delay);
            }

            yield return null;
        }

        while (behavior == BehaviorTypes.Descending)
        {
            if (Time.time < pause || delay == 0)
            {
                transform.Translate((Vector3.down * unit.speed) * Time.deltaTime);
            }
            else
            {
                yield return new WaitForSeconds(delay);
                pause = Time.time + delay;

            }

            yield return null;
        }
     
        targetLocation = GetRandomLocation();

        while (behavior == BehaviorTypes.Avoiding)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, unit.speed * Time.deltaTime);

            if (transform.position == targetLocation)
            {
                targetLocation = GetRandomLocation();
                yield return new WaitForSeconds(delay);
            }

            yield return null;
        }

    }
}
