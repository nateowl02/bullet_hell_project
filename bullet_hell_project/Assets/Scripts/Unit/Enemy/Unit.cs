using UnityEngine;
using System;
public class Unit : MonoBehaviour
{
    public event Action OnDeath;

    [Space]
    [Header("Player Stats")]
    public float healthMax = 1;
    public float healthCurrent;
    public float speed = .5f;
    public bool isDestructible = true;

    [Space]
    [Header("Collision")]
    public string collisionDamageTag;
    public float collisionDamage;

    //
    float damageCounter;
    bool isDead = false;
    //

    void Start()
    {
        healthCurrent = healthMax;
    }

    private void Update()
    {
        if (healthCurrent <= 0.0f && !isDead)
        {
            isDead = true;
            if (OnDeath != null) OnDeath();
            Destroy(gameObject);
        }
    }

    public string GetUnitTag() 
    {
        return gameObject.tag;
    }

    public void Damage(float in_damage)
    {
        if (isDestructible) healthCurrent -= in_damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == collisionDamageTag)
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit != null)
            { 
                unit.Damage(collisionDamage);
                DelayedDamage();
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == collisionDamageTag && Time.time > damageCounter)
        {
            Unit unit = other.GetComponent<Unit>();
            if (unit != null)
            { 
                unit.Damage(collisionDamage);
                DelayedDamage();
            }
        }
    }

    private void DelayedDamage()
    {
        damageCounter = Time.time + GameRules.collisionDamageDelay;
    }

}
