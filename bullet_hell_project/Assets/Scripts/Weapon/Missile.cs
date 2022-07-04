using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour
{
    public Transform model;

    [Header("Missile Properties")]
    public float rangeMax;
    public float rangeCurrent;
    public float speedStart;
    public float speedEnd;
    public float damage;
    public Vector3 direction;
    public string tagDamage;
    public float rotation;
    public float initialDelay;

    [Header("Special")]
    public bool isDelayedTracking;
    public float trackingDelay;
    public bool isPiercing;
    float trackingCounter;

    [Header("Polarity System")]
    public bool isPolarized = false;
    public PolaritySystem.Polarity currentPolarity;

    void FixedUpdate()
    {
        Move();
    }

    public void TrackingDelay()
    {
        if (!isDelayedTracking) return;
        trackingCounter = trackingDelay + Time.time;
        StartCoroutine("TrackingDelayCoroutine");
    }

    void Move()
    {
        model.rotation = Quaternion.Euler(0, 0, rotation + 180);
        transform.Translate(direction * (Mathf.Lerp(speedStart, speedEnd, rangeCurrent / rangeMax) * Time.deltaTime));
        if (!CheckRange()) Destroy(gameObject);
        
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

    bool CheckRange()
    {
        rangeCurrent += (Mathf.Lerp(speedStart, speedEnd, rangeCurrent / rangeMax) * Time.deltaTime);
        return rangeMax > rangeCurrent;
    }

    IEnumerator TrackingDelayCoroutine() 
    {
        while (Time.time < trackingCounter)
        {
            yield return null;
        }
        
        direction = EmpireanMath.GetTargetDirection(transform.position, direction, tagDamage, true);
        
        rotation = 180;
        rotation = rotation - EmpireanMath.GetAngleFromPoint(direction.x, direction.y);
        
    }
}
