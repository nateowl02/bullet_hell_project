using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    [Header("Missile Art")]
    public Missile missile;

    [Space]
    [Header("Weapon Properties")]
    public float damage = 1;
    public string damageTag;
    public float maxRange;
    public bool isPiercing = false;

    public bool GetObjectPosition(ref Vector3 position, string targetTag)
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag(targetTag);
        target = GetSortedArray(target);
        position = target.Length == 0 ? Vector3.zero : target[0].transform.position;
        
        return target.Length > 0;
    }

    public float GetStartingAngle(int turretCount, float spread, float startAngle)
    {
        for (int i = 0; i < turretCount / 2; i++)
        {
            startAngle -= spread;
        }

        if (turretCount % 2 == 0)
        {
            startAngle += spread / 2;
        }

        return startAngle;
    }

    public float GetStartingOffset(int turretCount, float offset)
    {
        float startOffset = 0;

        for (int i = 0; i < turretCount / 2; i++)
        {
            startOffset += offset;
        }

        if (turretCount % 2 == 0)
        {
            startOffset -= offset / 2;
        }

        return startOffset;
    }

    public void Shoot(MissileProperties missileProperties) 
    {
        Missile bullet = Instantiate(missile, missileProperties.Position, Quaternion.identity);
        bullet.direction = missileProperties.Direction;
        bullet.speedStart = missileProperties.StartSpeed;
        bullet.speedEnd = missileProperties.EndSpeed;
        bullet.rotation = missileProperties.Spread;
        bullet.damage = damage;
        bullet.tagDamage = damageTag;
        bullet.rangeMax = maxRange;
        bullet.isDelayedTracking = missileProperties.IsDelayedTracking;
        bullet.trackingDelay = missileProperties.TrackingDelay;
        bullet.isPiercing = isPiercing;
        bullet.TrackingDelay();

        
    }

    GameObject[] GetSortedArray(GameObject[] gameObject)
    { 
        return gameObject.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToArray();
    }

}
