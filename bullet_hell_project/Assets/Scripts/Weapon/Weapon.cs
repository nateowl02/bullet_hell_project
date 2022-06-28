using UnityEngine;
using System.Linq;

public class Weapon : MonoBehaviour
{
    [Header("Missile Art")]
    public Missile normalMissile;
    public Missile hope;
    public Missile despair;

    [Space]
    [Header("Weapon Properties")]
    public float damage = 1;
    public string damageTag;
    public float maxRange;
    public bool isPiercing = false;
    public bool isPolarized = false;
    PolaritySystem.Polarity polarity;

    //
    PolaritySystem unitPolarity;
    //

    public void Shoot(MissileProperties missileProperties) 
    {
        unitPolarity = gameObject.GetComponentInParent<PolaritySystem>();
        Missile bullet = Instantiate(isPolarized ? unitPolarity.currentPolarity == PolaritySystem.Polarity.hope ? hope : despair : normalMissile, missileProperties.Position, Quaternion.identity);
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
        bullet.isPolarized = isPolarized;
        bullet.currentPolarity = isPolarized ? unitPolarity.currentPolarity : PolaritySystem.Polarity.none;
        bullet.TrackingDelay();
    }
}
