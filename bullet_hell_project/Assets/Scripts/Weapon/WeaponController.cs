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
    [Header("Bullet Patterns")]
    public float patternDuration;
    public BulletPatterns bulletPattern;
    public int projectileCount;
    public float radius;

    public enum BulletPatterns 
    { 
        None,
        Circle
    }


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
                    spread: temp_angle + 90

                );

                object[] parms = { position, direction };

                switch (bulletPattern)
                {
                    case BulletPatterns.None:
                        weapon.Shoot(missile);
                        break;
                    case BulletPatterns.Circle:
                        StartCoroutine("DrawPattern", parms);
                        break;
                    default:
                        break;
                }
                
                temp_angle += fixedSpread;
                temp_offsetX += isInverted ? offsetX : -offsetX;
                temp_offsetY += isInverted ? offsetY : -offsetY;
            }

            yield return new WaitForSeconds(secondaryRateOfFire);
        }
    }

    IEnumerator DrawPattern(object[] parms) 
    {
        if (bulletPattern == BulletPatterns.Circle) 
        {
            Vector3 center = (Vector3) parms[0];
            Vector3 direction = (Vector3) parms[1];
            float fixedAngle = 360 / projectileCount;
            float currentAngle = 0;

            for (int i = 0; i < projectileCount; i++)
            {
                Vector3 position = GetDirectionFromAngle(currentAngle).normalized * radius;

                MissileProperties missile = new MissileProperties
                (
                    direction: direction,
                    position: center + position,
                    startSpeed: startSpeed,
                    endSpeed: endSpeed,
                    spread: currentAngle + 90,
                    initialDelay: initialDelay
                );

                weapon.Shoot(missile);
                currentAngle += fixedAngle;
                yield return new WaitForSeconds(patternDuration);

            }

        }
    }

    float GetAngleFromPoint(float x, float y)
    {
        return Mathf.Atan2(x, y) * Mathf.Rad2Deg;
    }

    Vector3 GetDirectionFromAngle(float angle)
    {
        return new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
    }
}
