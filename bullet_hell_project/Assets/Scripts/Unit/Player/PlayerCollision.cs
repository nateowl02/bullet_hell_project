﻿using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private UnitProperties unitProperties;

    protected float damageCounter;

    private void Start()
    {
        if (TryGetComponent(out Player player))
        {
            unitProperties = player.UnitProperties;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == unitProperties.collisionDamageTag)
        {
            DelayedDamage(other);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(unitProperties.collisionDamageTag) && Time.time > damageCounter)
        {
            DelayedDamage(other);
        }
    }

    private void DelayedDamage(Collider2D other)
    {

        if (other.TryGetComponent(out IUnit unit))
        {
            unit.TakeDamage(unitProperties.collisionDamage);
        }
        damageCounter = Time.time + GameRules.collisionDamageDelay;
    }
}
