using System.Collections;
using System.Collections.Generic;
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
            (weaponType.startInitialAngle, weaponType.endInitialAngle) = (weaponType.endInitialAngle, weaponType.startInitialAngle);
            (weaponType.startSpread, weaponType.endSpread) = (weaponType.endSpread, weaponType.startSpread);
            (weaponType.startFinalAngle, weaponType.endFinalAngle) = (weaponType.endFinalAngle, weaponType.startFinalAngle);
        }

        stepCount = (float)weaponType.secondaryTurretCount / ((float) weaponType.secondaryTurretCount - 1 == 0 ? 1 : weaponType.secondaryTurretCount - 1);

        for (int i = 0; i < weaponType.secondaryTurretCount; i++)
        {
            if (weaponType.isLocking)
            {
                Vector3 targetDirection = EmpireanMath.GetTargetDirection(transform.position, Vector3.zero, weaponType.damageTag, true);
                aimAdjustment = EmpireanMath.GetAngleFromPoint(targetDirection.x, targetDirection.y);

            }
            else
                aimAdjustment = 0;

            progress = ((float)i * stepCount) / (float)weaponType.secondaryTurretCount;


            float[] startAngles = EmpireanMath.GetAngles(
                    weaponType.primaryTurretCount,
                    Mathf.Lerp(weaponType.startSpread, weaponType.endSpread, progress),
                    Mathf.Lerp(weaponType.startInitialAngle, weaponType.endInitialAngle, progress),
                    aimAdjustment
                );

            float[] endAngles = EmpireanMath.GetAngles(
                    weaponType.primaryTurretCount,
                    Mathf.Lerp(weaponType.startSpread, weaponType.endSpread, progress),
                    Mathf.Lerp(weaponType.startFinalAngle, weaponType.endFinalAngle, progress),
                    aimAdjustment
                );

            float temp_offsetX = EmpireanMath.GetStartingOffset(weaponType.primaryTurretCount, weaponType.offsetX);
            float temp_offsetY = EmpireanMath.GetStartingOffset(weaponType.primaryTurretCount, weaponType.offsetY);

            temp_offsetX = weaponType.isInverted ? temp_offsetX * -1 : temp_offsetX;
            temp_offsetY = weaponType.isInverted ? temp_offsetY * -1 : temp_offsetY;

            for (int j = 0; j < weaponType.primaryTurretCount; j++)
            {
                startAngles[j] = weaponType.isInverted ? startAngles[j] + 180 : startAngles[j];

                Vector3 position = transform.position + new Vector3(temp_offsetX, temp_offsetY, 0);
                Vector3 initialDirection = EmpireanMath.GetDirectionFromAngle(startAngles[j]).normalized;
                Vector3 finalDirection = EmpireanMath.GetDirectionFromAngle(endAngles[j]).normalized;

                unitPolarity = gameObject.GetComponentInParent<PolaritySystem>();

                Projectile projectile = Instantiate(weaponType.isPolarized ? unitPolarity.currentPolarity == PolaritySystem.Polarity.hope ? weaponType.hope : weaponType.despair : weaponType.normalMissile, position, Quaternion.identity);

                // Range
                projectile.rangeMax = weaponType.maxRange;
                projectile.homingDistance = weaponType.homingDistance;

                /// Speed
                projectile.speedStart = weaponType.startSpeed;
                projectile.speedEnd = weaponType.endSpeed;

                // Damage
                projectile.damage = weaponType.damage;
                projectile.tagDamage = weaponType.damageTag;

                // Direction
                projectile.direction = initialDirection;

                // Rotation
                projectile.rotation = startAngles[j] + 90;

                // Delay
                projectile.movementDelay = weaponType.movementDelay;
                projectile.homingDelay = weaponType.homingDelay;
                projectile.homingInterval = weaponType.homingInterval;

                // Polarity
                projectile.isPiercing = weaponType.isPiercing;
                projectile.isPolarized = weaponType.isPolarized;
                projectile.currentPolarity = weaponType.isPolarized ? unitPolarity.currentPolarity : PolaritySystem.Polarity.none;

                // curve
                projectile.initialDirection = initialDirection;
                projectile.finalDirection = finalDirection;
                
                List<Vector3> curve = new List<Vector3>();
                for (int m = 0; m < weaponType.curveAngle.Length; m++)
                {
                    curve.Add(EmpireanMath.GetDirectionFromAngle(weaponType.curveAngle[m]).normalized);
                }
                projectile.curve = curve.ToArray();

                temp_offsetX += weaponType.isInverted ? weaponType.offsetX : -weaponType.offsetX;
                temp_offsetY += weaponType.isInverted ? weaponType.offsetY : -weaponType.offsetY;
            }

            yield return new WaitForSeconds(weaponType.secondaryRateOfFire);
        }
    }
}
