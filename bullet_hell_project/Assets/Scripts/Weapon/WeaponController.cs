using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Weapon))]
public class WeaponController : MonoBehaviour
{
    [Space]
    [Header("Weapon Properties")]
    public int primaryTurretCount;
    public int secondaryTurretCount;
    public float primaryRateOfFire;
    public float secondaryRateOfFire;
    public float fixedSpread;
    public float startSpread;
    public float endSpread;
    public float startSpeed;
    public float endSpeed;
    public float initialDelay;
    public string targetTag;

    [Space]
    [Header("Offset")]
    public float offsetX;
    public float offsetY;

    [Space]
    [Header("Weapon Modes")]
    public bool isInverted = false;
    public bool isLocking = false;
    public bool isFixed = true;
    public bool isOrbiting = false;

    [Space]
    [Header("Bullet Special")]
    public bool isDelayedTracking = false;
    public float trackingDelay;
    public bool isRetaliating = false;

    Weapon weapon;
    float fireCounter;

    void Start()
    {
        weapon = GetComponent<Weapon>();
        fireCounter = Time.time + initialDelay;

    }

    void Update()
    {
        if (Time.time >= fireCounter)
        {
            Shoot();
            fireCounter = Time.time + primaryRateOfFire;
        }
    }

    IEnumerator ShootingController()
    {

        for (int i = 0; i < secondaryTurretCount; i++)
        {
            float temp_angle = isFixed ? EmpireanMath.GetStartingAngle(primaryTurretCount, fixedSpread, 90) : EmpireanMath.GetStartingAngle(primaryTurretCount, fixedSpread, Mathf.Lerp(startSpread, endSpread, (float)i / (float)(secondaryTurretCount - 1)));
            float temp_offsetX = EmpireanMath.GetStartingOffset(primaryTurretCount, offsetX);
            float temp_offsetY = EmpireanMath.GetStartingOffset(primaryTurretCount, offsetY);

            if (isLocking)
            {
                Vector3 targetLocation = EmpireanMath.GetTarget(transform.position, targetTag, true);
                Vector3 temp_direction = targetLocation - transform.position;
                temp_angle = temp_angle - EmpireanMath.GetAngleFromPoint(temp_direction.x, temp_direction.y);

            }

            if (isOrbiting)
            {
                Vector3 temp_direction = transform.position - transform.parent.position;
                temp_angle = temp_angle - EmpireanMath.GetAngleFromPoint(temp_direction.x, temp_direction.y);
            }
            
            temp_angle = isInverted ? temp_angle + 180 : temp_angle;
            temp_offsetX = isInverted ? temp_offsetX * -1 : temp_offsetX;
            temp_offsetY = isInverted ? temp_offsetY * -1 : temp_offsetY;

            for (int j = 0; j < primaryTurretCount; j++)
            {
                Vector3 position = weapon.transform.position + new Vector3(temp_offsetX, temp_offsetY, 0);
                Vector3 direction = EmpireanMath.GetDirectionFromAngle(temp_angle).normalized;

                MissileProperties missile = new MissileProperties
                (
                    direction: direction,
                    position: position,
                    startSpeed: startSpeed,
                    endSpeed: endSpeed,
                    spread: temp_angle + 90,
                    isDelayedTracking: isDelayedTracking,
                    trackingDelay: trackingDelay
                );

                object[] parms = { position, direction };

                weapon.Shoot(missile);

                temp_angle += fixedSpread;
                temp_offsetX += isInverted ? offsetX : -offsetX;
                temp_offsetY += isInverted ? offsetY : -offsetY;
            }

            yield return new WaitForSeconds(secondaryRateOfFire);
        }
    }

    void Shoot()
    {
        StartCoroutine("ShootingController");
    }

}
