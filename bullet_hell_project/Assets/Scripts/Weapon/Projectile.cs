using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Properties")]
    protected float rangeMax;
    protected float rangeCurrent;
    protected float speedStart;
    protected float speedEnd;
    protected string tagDamage;
    protected float damage;

    void FixedUpdate()
    {
        Move();
        if (!CheckRange()) Destroy(gameObject);
    }

    bool CheckRange()
    {
        if (rangeCurrent < rangeMax)
        {
            rangeCurrent = rangeCurrent + (Mathf.Lerp(speedStart, speedEnd, rangeCurrent / rangeMax) * Time.deltaTime);
            return true;
        }
        return false;
    }

    void Move()
    {
        transform.Translate(Vector3.up * Mathf.Lerp(speedStart, speedEnd, rangeCurrent / rangeMax) * Time.deltaTime);
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

    public void SetStartSpeed(float in_speed)
    {
        speedStart = in_speed;
    }

    public void SetEndSpeed(float in_speed)
    {
        speedEnd = in_speed;
    }

    public void SetDamage(float in_damage)
    {
        damage = in_damage;
    }

    public void SetMaxRange(float in_range)
    {
        rangeMax = in_range;
    }

    public void SetDamageTag(string in_damageTag)
    {
        tagDamage = in_damageTag;

    }

}
