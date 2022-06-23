using UnityEngine;
using System.Linq;
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

    public bool isDelayedTracking;
    public float trackingDelay;
    public bool isPiercing;
    float trackingCounter;

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
        model.rotation = Quaternion.Euler(0,0, rotation + 180);
        transform.Translate(direction * (Mathf.Lerp(speedStart, speedEnd, rangeCurrent / rangeMax) * Time.deltaTime));
        if (!CheckRange()) Destroy(gameObject);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == tagDamage)
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit != null) unit.Damage(damage);
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
      
        GameObject[] target = GameObject.FindGameObjectsWithTag(tagDamage);
        target = target.OrderBy(x => Vector3.Distance(this.transform.position, x.transform.position)).ToArray();
        direction = target.Length > 0 ? (target[0].transform.position - transform.position).normalized : direction;
        rotation = 180;
        rotation = rotation - WeaponController.GetAngleFromPoint(direction.x, direction.y);
    }
}
