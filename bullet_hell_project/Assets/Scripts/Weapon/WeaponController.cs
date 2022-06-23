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
            StartCoroutine("ShootingController");

            fireCounter = Time.time + primaryRateOfFire;
        }
    }

    IEnumerator ShootingController()
    {

        for (int i = 0; i < secondaryTurretCount; i++)
        {
            
            
            float temp_angle = isFixed ? weapon.GetStartingAngle(primaryTurretCount, fixedSpread, 90) : weapon.GetStartingAngle(primaryTurretCount, fixedSpread, Mathf.Lerp(startSpread, endSpread, (float)i / (float)(secondaryTurretCount - 1)));
            float temp_offsetX = weapon.GetStartingOffset(primaryTurretCount, offsetX);
            float temp_offsetY = weapon.GetStartingOffset(primaryTurretCount, offsetY);

            if (isLocking)
            {
                Vector3 v = Vector3.up;
                if (weapon.GetObjectPosition(ref v, targetTag))
                {
                    Vector3 temp_direction = v - transform.position;
                    temp_angle = temp_angle - GetAngleFromPoint(temp_direction.x, temp_direction.y);
                }
            }

            if (isOrbiting)
            {
                Vector3 temp_direction = transform.position - transform.parent.position;
                temp_angle = temp_angle - GetAngleFromPoint(temp_direction.x, temp_direction.y);
            }
            
            temp_angle = isInverted ? temp_angle + 180 : temp_angle;
            temp_offsetX = isInverted ? temp_offsetX * -1 : temp_offsetX;
            temp_offsetY = isInverted ? temp_offsetY * -1 : temp_offsetY;

            for (int j = 0; j < primaryTurretCount; j++)
            {
                Vector3 position = weapon.transform.position + new Vector3(temp_offsetX, temp_offsetY, 0);
                Vector3 direction = GetDirectionFromAngle(temp_angle).normalized;

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

    public static float GetAngleFromPoint(float x, float y)
    {
        return Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }

    public static Vector3 GetDirectionFromAngle(float angle)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }
}
