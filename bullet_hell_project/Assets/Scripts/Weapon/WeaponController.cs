using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Weapon weaponType;

    float fireCounter;
    float aimAdjustment;
    float stepCount;
    float progress;

    PolaritySystem.Polarity polarity;
    PolaritySystem unitPolarity;

    void Start()
    {
        fireCounter = Time.time + weaponType.initialDelay;
    }

    void Update()
    {

        if (Time.time >= fireCounter)
        {
            Shoot();
            fireCounter = Time.time + weaponType.primaryRateOfFire;
        }
    }

    void Shoot()
    {
        StartCoroutine("FiringController");
    }

    IEnumerator FiringController()
    {
        if (weaponType.isCycling)
        {
            (weaponType.startAngle, weaponType.endAngle) = (weaponType.endAngle, weaponType.startAngle);
            (weaponType.startSpread, weaponType.endSpread) = (weaponType.endSpread, weaponType.startSpread);
        }

        stepCount = (float)weaponType.secondaryTurretCount / ((float)weaponType.secondaryTurretCount - 1);

        for (int i = 0; i < weaponType.secondaryTurretCount; i++)
        {
            if (weaponType.isLocking)
            {
                Vector3 targetDirection = EmpireanMath.GetTargetDirection(transform.position, transform.position, weaponType.damageTag, true);
                aimAdjustment = EmpireanMath.GetAngleFromPoint(targetDirection.x, targetDirection.y);

            }

            progress = ((float)i * stepCount) / (float)weaponType.secondaryTurretCount;


            float[] angles = EmpireanMath.GetAngles(
                    weaponType.primaryTurretCount,
                    Mathf.Lerp(weaponType.startSpread, weaponType.endSpread, progress),
                    Mathf.Lerp(weaponType.startAngle, weaponType.endAngle, progress),
                    aimAdjustment
                );

            float temp_offsetX = EmpireanMath.GetStartingOffset(weaponType.primaryTurretCount, weaponType.offsetX);
            float temp_offsetY = EmpireanMath.GetStartingOffset(weaponType.primaryTurretCount, weaponType.offsetY);

            temp_offsetX = weaponType.isInverted ? temp_offsetX * -1 : temp_offsetX;
            temp_offsetY = weaponType.isInverted ? temp_offsetY * -1 : temp_offsetY;

            for (int j = 0; j < weaponType.primaryTurretCount; j++)
            {
                angles[j] = weaponType.isInverted ? angles[j] + 180 : angles[j];

                Vector3 position = transform.position + new Vector3(temp_offsetX, temp_offsetY, 0);
                Vector3 direction = EmpireanMath.GetDirectionFromAngle(angles[j]).normalized;

                unitPolarity = gameObject.GetComponentInParent<PolaritySystem>();

                Missile bullet = Instantiate(weaponType.isPolarized ? unitPolarity.currentPolarity == PolaritySystem.Polarity.hope ? weaponType.hope : weaponType.despair : weaponType.normalMissile, position, Quaternion.identity);

                // Range
                bullet.rangeMax = weaponType.maxRange;
                bullet.homingDistance = weaponType.homingDistance;

                /// Speed
                bullet.speedStart = weaponType.startSpeed;
                bullet.speedEnd = weaponType.endSpeed;

                // Damage
                bullet.damage = weaponType.damage;
                bullet.tagDamage = weaponType.damageTag;

                // Direction
                bullet.direction = direction;

                // Rotation
                bullet.rotation = angles[j] + 90;

                // Delay
                bullet.movementDelay = weaponType.movementDelay;
                bullet.homingDelay = weaponType.homingDelay;
                bullet.homingInterval = weaponType.homingInterval;

                // Polarity
                bullet.isPiercing = weaponType.isPiercing;
                bullet.isPolarized = weaponType.isPolarized;
                bullet.currentPolarity = weaponType.isPolarized ? unitPolarity.currentPolarity : PolaritySystem.Polarity.none;

                temp_offsetX += weaponType.isInverted ? weaponType.offsetX : -weaponType.offsetX;
                temp_offsetY += weaponType.isInverted ? weaponType.offsetY : -weaponType.offsetY;
            }

            yield return new WaitForSeconds(weaponType.secondaryRateOfFire);
        }
    }
}
