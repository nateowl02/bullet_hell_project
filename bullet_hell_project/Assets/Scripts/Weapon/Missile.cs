using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public enum ProjectileType { 
        linear,
        pattern,  
        piercing,
        homing,
    }

    [Header("Missile Properties")]
    public float rangeMax;
    public float rangeCurrent;
    public float speedStart;
    public float speedEnd;
    public float damage;
    public Vector3 direction;
    public string tagDamage;
    public ProjectileType type;

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * (Mathf.Lerp(speedStart, speedEnd, rangeCurrent / rangeMax) * Time.deltaTime));
        if (!CheckRange()) Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == tagDamage)
        {
            Unit unit = other.GetComponent<Unit>();
            unit.Damage(damage);

            Destroy(gameObject);
        }
    }

    bool CheckRange()
    {
        rangeCurrent += (Mathf.Lerp(speedStart, speedEnd, rangeCurrent / rangeMax) * Time.deltaTime);
        return rangeMax > rangeCurrent;
    }
}
