using UnityEngine;

[RequireComponent(typeof(Unit))]
public class TimeJuiceDrop : MonoBehaviour
{
    public TImeJuice timeJuice;
    Unit unit;
    private void Start()
    {
        unit = GetComponent<Unit>();
        unit.OnDeath += SpawnTimeJuice;
    }

    void SpawnTimeJuice()
    {
        Instantiate(timeJuice, transform.position, Quaternion.identity);
    }
}
