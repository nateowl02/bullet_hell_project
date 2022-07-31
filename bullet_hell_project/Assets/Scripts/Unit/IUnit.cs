using UnityEngine;

public interface IUnit
{
    UnitProperties UnitProperties { get; set; }

    void TakeDamage(float damage);
}
