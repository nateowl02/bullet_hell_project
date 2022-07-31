using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class MathUtils
{
    public static Vector3 GetDirectionFromAngle(float angle)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }

    public static float GetAngleFromPoint(float x, float y)
    {
        return Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }

    public static Vector3 GetTargetDirection(Vector3 origin, Vector3 originalDirection, string targetTag, bool isClosest)
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag(targetTag);
        target = isClosest ? GetSortedArray(origin, target) : target;
        return target.Length > 0 ? (target[0].transform.position - origin).normalized : originalDirection;
    }

    static GameObject[] GetSortedArray(Vector3 origin, GameObject[] gameObject)
    {
        return gameObject.OrderBy(x => Vector3.Distance(origin, x.transform.position)).ToArray();
    }

    public static float GetStartingOffset(int turretCount, float offset)
    {
        float startOffset = 0;

        for (int i = 0; i < turretCount / 2; i++)
            startOffset += offset;

        if (turretCount % 2 == 0)
            startOffset -= offset / 2;

        return startOffset;
    }

    public static Vector3 GetRandomLocation()
    {
        return new Vector3(Random.Range(-GameRules.screenWidth, GameRules.screenWidth), Random.Range(GameRules.screenHeight, GameRules.screenHeight / 2), 0);
    }

    public static float[] GetAngles(int turretCount, float spread, float startAngle, float aimAdjustment = 0)
    {
        List<float> angles = new List<float>();

        startAngle -= spread * (turretCount / 2);
        if (turretCount % 2 == 0)
            startAngle += (spread / 2);
            
        for (int i = 0; i < turretCount; i++)
        {
            angles.Add(startAngle - aimAdjustment);
            startAngle += spread ;
        }

        return angles.ToArray();
    }
}
