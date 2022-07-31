using System.Collections.Generic;
using UnityEngine;


public class Unit : MonoBehaviour
{
    public UnitProperties unit;

    protected float currentHealth;
    protected float damageCounter;
    protected bool isDead = false;

    public static List<Unit> allUnits = new List<Unit>();

    void Start()
    {
        currentHealth = unit.healthMax;
        allUnits.Add(this);
    }

    public void TakeDamage(float damage)
    {
        if (unit.isDestructible) currentHealth -= damage;
    }

    void Update()
    {
        if (currentHealth <= 0.0f && !isDead)
        {
            isDead = true;
            // if (OnDeath != null) OnDeath();
            Destroy(gameObject);
        }
    }
}
