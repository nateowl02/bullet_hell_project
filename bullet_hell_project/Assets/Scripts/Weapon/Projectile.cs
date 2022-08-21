using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
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
    public float movementDelay;
    public float homingDelay;
    public float homingInterval;
    // piercing
    public bool isPiercing;

    [Header("Polarity System")]
    public bool isPolarized = false;
    public PolaritySystem.Polarity currentPolarity;

    [Header("Curve")]
    public float curveMaxDistance;
    public Vector3 initialDirection;
    public Vector3 finalDirection;
    public Vector3[] curve;

    // counters
    float homingCounter;
    float rangeCounter;
    float rangeProgress;
    float movementCounter;
    float currentSpeed;
    float homingCurrentDistance;
    

    private void Start()
    {
        movementCounter = Time.time + movementDelay;
        homingCounter = Time.time + homingDelay;
        StartCoroutine("DirectionCorrection");
    }

    void FixedUpdate()
    {
        if (Time.time <= movementCounter) return;
        rangeProgress = rangeCounter / rangeMax;
        Move();
    }
    
    void Move()
    {
        model.rotation = Quaternion.Euler(0, 0, rotation + 180);
        currentSpeed = Mathf.Lerp(speedStart, speedEnd, rangeProgress);
        transform.Translate(direction * currentSpeed * Time.deltaTime);
        if (!CheckRange()) Destroy(gameObject);
        
    }

    bool CheckRange()
    {
        rangeCounter += (Mathf.Lerp(speedStart, speedEnd, rangeProgress) * Time.deltaTime);
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
                        unit.TakeDamage(damage * PolaritySystem.polarityDamageFactor);
                    else
                        unit.TakeDamage(damage);
                }
                
                if (tagDamage == "Player")
                {
                    if (polarity.currentPolarity != currentPolarity) unit.TakeDamage(damage);
                    else FindObjectOfType<HopeUI>().OnAbsorb(currentPolarity);
                    
                }
                
            }
            else
            {
                IUnit unit = other.GetComponent<IUnit>();
                if (unit != null) unit.TakeDamage(damage);
            }
            if (!isPiercing) Destroy(gameObject);
        }
    }

   
    IEnumerator DirectionCorrection() 
    {
        while (Time.time < homingCounter)
        {
            
            Vector3 temp_point;
            Vector3 start_point = initialDirection;
            Vector3 end_point = finalDirection;

            for (int i = 0; i < curve.Length; i++)
            {
                temp_point = Vector3.Lerp(start_point, curve[i], rangeProgress);
                start_point = temp_point;
            }

            direction = Vector3.Lerp(start_point, end_point, rangeProgress);
            AdjustRotation();
            
            yield return null;

        }

        while (homingCurrentDistance < homingDistance)
        {
            homingCurrentDistance += currentSpeed * Time.deltaTime;
            Vector3 targetDirection = MathUtils.GetTargetDirection(transform.position, direction, tagDamage, true);
            direction = Vector3.Lerp(direction, targetDirection, homingCurrentDistance / homingDistance);
            AdjustRotation();
            yield return new WaitForSeconds(homingInterval);
        }
        
        direction = MathUtils.GetTargetDirection(transform.position, direction, tagDamage, true);
        AdjustRotation();
    }

    private void AdjustRotation()
    {
        rotation = 180;
        rotation = rotation - MathUtils.GetAngleFromPoint(direction.x, direction.y);
    }
}
