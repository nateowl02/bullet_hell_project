using UnityEngine;

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
    public float movementDelay = 0.0f;

    //
    PolaritySystem.Polarity polarity;
    PolaritySystem unitPolarity;
    //

    public void Shoot(MissileProperties missileProperties) 
    {
        unitPolarity = gameObject.GetComponentInParent<PolaritySystem>();

        Missile bullet = Instantiate(isPolarized ? unitPolarity.currentPolarity == PolaritySystem.Polarity.hope ? hope : despair : normalMissile, missileProperties.Position, Quaternion.identity);

        // Range
        bullet.rangeMax = maxRange;
        bullet.homingDistance = missileProperties.HomingDistance;

        /// Speed
        bullet.speedStart = missileProperties.StartSpeed;
        bullet.speedEnd = missileProperties.EndSpeed;

        // Damage
        bullet.damage = damage;
        bullet.tagDamage = damageTag;

        // Direction
        bullet.direction = missileProperties.Direction;

        // Rotation
        bullet.rotation = missileProperties.Spread;

        // Delay
        bullet.trackingDelay = missileProperties.TrackingDelay;
        bullet.movementDelay = movementDelay;
        bullet.homingDelay = missileProperties.HomingDelay;
        bullet.homingInterval = missileProperties.HomingInterval;

        // Polarity
        bullet.isPiercing = isPiercing;
        bullet.isPolarized = isPolarized;
        bullet.currentPolarity = isPolarized ? unitPolarity.currentPolarity : PolaritySystem.Polarity.none;
    }
}
