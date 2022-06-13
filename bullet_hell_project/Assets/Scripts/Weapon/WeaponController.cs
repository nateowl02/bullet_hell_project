using System.Collections;
using System.Collections.Generic;
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
    public float startSpread;
    public float endSpread;
    public float startSpeed;
    public float endSpeed;
    public float offset;
    public float initialDelay;
    public string targetTag;

    [Space]
    [Header("Weapon Modes")]
    public bool isInverted = false;
    public bool isLocking = false;

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
        // Mathf.Lerp(startSpread, endSpread, (float)i / secondaryTurretCount)
        for (int i = 0; i < secondaryTurretCount; i++)
        {
            float temp_angle = weapon.GetStartingAngle(primaryTurretCount, startSpread);
            float temp_offset = weapon.GetStartingOffset(primaryTurretCount, offset);

            if (isLocking) 
            {
                Vector3 v = Vector3.up;
                weapon.GetObjectPosition(ref v, targetTag);
                Vector3 temp_direction = v - transform.position;
                temp_direction = temp_direction.normalized;
                temp_angle = temp_angle - Mathf.Atan2(temp_direction.x, temp_direction.y) * Mathf.Rad2Deg;
            }
            
            temp_angle = isInverted ? temp_angle + 180 : temp_angle;
            temp_offset = isInverted ? temp_offset * -1 : temp_offset;

            for (int j = 0; j < primaryTurretCount; j++)
            {

                weapon.Shoot(new MissileProperties(
                    direction: new Vector3(Mathf.Cos(temp_angle * Mathf.Deg2Rad), Mathf.Sin(temp_angle * Mathf.Deg2Rad), 0),
                    position: transform.position + new Vector3(temp_offset, 0, 0),
                    startSpeed: startSpeed,
                    endSpeed: endSpeed,
                    spread: temp_angle + 90
                    )

                );

                temp_angle += startSpread;
                temp_offset += isInverted ? offset : -offset;
            }

            yield return new WaitForSeconds(secondaryRateOfFire);
        }
  

    }
}
