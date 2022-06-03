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
    public float spread;
    public float startSpeed;
    public float endSpeed;
    public float offset;
    public float initialDelay;
    public string targetTag;

    [Space]
    [Header("Weapon Modes")]
    public bool isInverted = false;
    public enum WeaponModes
    { 
        None,
        Directional,
        Directed

    };

    public WeaponModes weaponMode;

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
        if (weaponMode == WeaponModes.Directional)
        {
            for (int i = 0; i < secondaryTurretCount; i++)
            {

                float temp_angle = weapon.GetStartingAngle(primaryTurretCount, spread);
                float temp_offset = weapon.GetStartingOffset(primaryTurretCount, offset);

                temp_angle = isInverted ? temp_angle + 180 : temp_angle;
                temp_offset = isInverted ? temp_offset * -1 : temp_offset;

                for (int j = 0; j < primaryTurretCount; j++)
                {

                    weapon.Shoot(temp_angle, temp_offset, startSpeed, endSpeed);

                    temp_angle += spread;
                    temp_offset += isInverted ? offset : -offset;

                }

                yield return new WaitForSeconds(secondaryRateOfFire);
            }

        }

        if (weaponMode == WeaponModes.Directed)
        {
            Vector3 v = new Vector3();
            bool isFound = weapon.GetObjectPosition(ref v, targetTag);

            for (int i = 0; i < secondaryTurretCount; i++)
            {
                float temp_angle = weapon.GetStartingAngle(primaryTurretCount, spread);

                for (int j = 0; j < primaryTurretCount; j++)
                {

                    if (isFound && v.x != transform.position.x)
                    {
                        weapon.Shoot(v, temp_angle, 0, startSpeed, endSpeed);
                    }
                    else
                    {
                        weapon.Shoot(isInverted == true ? temp_angle + 180 : temp_angle, 0, startSpeed, endSpeed);
                    }

                    temp_angle += spread;

                }

                yield return new WaitForSeconds(secondaryRateOfFire);
            }
        }

    }
}
