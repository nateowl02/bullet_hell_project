using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public Transform model;
    // range
    public float rangeMax;
    public float homingDistance;
    // speed
    public float speedStart;
    public float speedEnd;
    // damage
    public float damage;
    public string tagDamage;
    // direction
    public Vector3 direction;
    // roation
    public float rotation;
    // delay
    public float trackingDelay;
    public float movementDelay;
    public float homingDelay;
    public float homingInterval;
    // piercing
    public bool isPiercing;

    [Header("Polarity System")]
    public bool isPolarized = false;
    public PolaritySystem.Polarity currentPolarity;

    // counters
    float trackingCounter;
    float homingCounter;
    float rangeCounter;
    float movementCounter;
    float currentSpeed;
    float homingCurrentDistance;

    private void Start()
    {
        movementCounter = Time.time + movementDelay;
        trackingCounter =  Time.time + trackingDelay;
        homingCounter = Time.time + trackingDelay + homingDelay;
        StartCoroutine("DirectionCorrection");
    }

    void FixedUpdate()
    {
        if (Time.time <= movementCounter) return;
        Move();
    }

    void Move()
    {
        model.rotation = Quaternion.Euler(0, 0, rotation + 180);
        currentSpeed = Mathf.Lerp(speedStart, speedEnd, rangeCounter / rangeMax);
        transform.Translate(direction * currentSpeed * Time.deltaTime);
        if (!CheckRange()) Destroy(gameObject);
        
    }

    bool CheckRange()
    {
        rangeCounter += (Mathf.Lerp(speedStart, speedEnd, rangeCounter / rangeMax) * Time.deltaTime);
        return rangeMax > rangeCounter;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == tagDamage)
        {
            
            if (isPolarized)
            {
                Unit unit = other.GetComponentInParent<Unit>();
                PolaritySystem polarity = unit.GetComponent<PolaritySystem>();
                
                if (tagDamage == "Enemy")
                {

                    if (polarity.currentPolarity != currentPolarity)
                        unit.Damage(damage * PolaritySystem.polarityDamageFactor);
                    else
                        unit.Damage(damage);
                }
                
                if (tagDamage == "Player")
                {
                    if (polarity.currentPolarity != currentPolarity) unit.Damage(damage);
                    else GameObject.FindObjectOfType<HopeUI>().OnAbsorb(currentPolarity);
                    
                }
                
            }
            else
            {
                Unit unit = other.GetComponent<Unit>();
                if (unit != null) unit.Damage(damage);
            }
            if (!isPiercing) Destroy(gameObject);
        }
    }

   
    IEnumerator DirectionCorrection() 
    {

        while (Time.time < trackingCounter)
        {
            
            yield return null;
        }

        while (Time.time < homingCounter)
        {
            yield return null;
        }

        while (homingCurrentDistance < homingDistance)
        {
            homingCurrentDistance += currentSpeed * Time.deltaTime;
            Vector3 targetDirection = EmpireanMath.GetTargetDirection(transform.position, direction, tagDamage, true);
            direction = Vector3.Lerp(direction, targetDirection, homingCurrentDistance / homingDistance);
            AdjustRotation();
            yield return new WaitForSeconds(homingInterval);
        }

        direction = EmpireanMath.GetTargetDirection(transform.position, direction, tagDamage, true);
        AdjustRotation();
    }

    private void AdjustRotation()
    {
        rotation = 180;
        rotation = rotation - EmpireanMath.GetAngleFromPoint(direction.x, direction.y);
    }
}
