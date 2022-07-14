using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Weapon))]
public class WeaponController : MonoBehaviour
{
    [Space]
    [Header("Weapon Specs")]
    public int primaryTurretCount;
    public int secondaryTurretCount;
   
    [Space]
    [Header("Initial Spread")]
    public float startAngle;
    public float endAngle;

    [Space]
    [Header("Secondary Spread")]
    public float startSpread;
    public float endSpread;

    [Space]
    [Header("Speed")]
    public float startSpeed;
    public float endSpeed;

    [Space]
    [Header("Delay")]
    public float initialDelay;
    public float primaryRateOfFire;
    public float secondaryRateOfFire;

    [Space]
    [Header("Offset")]
    public float offsetX;
    public float offsetY;

    [Space]
    [Header("Weapon Modes")]
    public bool isInverted = false;
    public bool isLocking = false;

    [Space]
    [Header("Delay")]
    public float homingDelay = 0.0f;
    public float homingInterval = 0.0f;
    public float homingDistance = 1.0f;

    Weapon weapon;
    float fireCounter;
    float aimAdjustment;
    float stepCount;
    float progress;

    void Start()
    {
        weapon = GetComponent<Weapon>();
        fireCounter = Time.time + initialDelay;

    }

    void Update()
    {

        /*
        if (Time.time >= fireCounter)
        {
            Shoot();
            fireCounter = Time.time + primaryRateOfFire;
        }
        */

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Shoot();
        }
    }

    void Shoot()
    {
        StartCoroutine("FiringController");
    }

    IEnumerator FiringController()
    {

        stepCount = (float) secondaryTurretCount / ((float)secondaryTurretCount-1);
        
        for (int i = 0; i < secondaryTurretCount; i++)
        {
            if (isLocking)
            {
                Vector3 targetDirection = EmpireanMath.GetTargetDirection(transform.position, transform.position, weapon.damageTag, true);
                aimAdjustment = EmpireanMath.GetAngleFromPoint(targetDirection.x, targetDirection.y);

            }

            progress = ((float)i * stepCount) / (float)secondaryTurretCount;

            float[] angles = EmpireanMath.GetAngles(
                    primaryTurretCount, 
                    Mathf.Lerp(startSpread, endSpread, progress), 
                    Mathf.Lerp(startAngle, endAngle, progress),
                    aimAdjustment
                );

            float temp_offsetX = EmpireanMath.GetStartingOffset(primaryTurretCount, offsetX);
            float temp_offsetY = EmpireanMath.GetStartingOffset(primaryTurretCount, offsetY);
            
            temp_offsetX = isInverted ? temp_offsetX * -1 : temp_offsetX;
            temp_offsetY = isInverted ? temp_offsetY * -1 : temp_offsetY;

            for (int j = 0; j < primaryTurretCount; j++)
            {
                angles[j] = isInverted ? angles[j] + 180 : angles[j];

                Vector3 position = weapon.transform.position + new Vector3(temp_offsetX, temp_offsetY, 0);
                Vector3 direction = EmpireanMath.GetDirectionFromAngle(angles[j]).normalized;

                MissileProperties missile = new MissileProperties
                (
                    direction: direction,
                    position: position,
                    startSpeed: startSpeed,
                    endSpeed: endSpeed,
                    spread: angles[j] + 90,
                    homingDelay: homingDelay,
                    homingInterval: homingInterval,
                    homingDistance: homingDistance
                );

                weapon.Shoot(missile);

                temp_offsetX += isInverted ? offsetX : -offsetX;
                temp_offsetY += isInverted ? offsetY : -offsetY;
            }

            yield return new WaitForSeconds(secondaryRateOfFire);
        }
    }

}
