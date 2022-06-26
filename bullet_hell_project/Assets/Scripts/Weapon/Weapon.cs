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
}
