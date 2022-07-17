using UnityEngine;

[RequireComponent(typeof(Unit))]
public class Player : MonoBehaviour
{
    Unit unit;

    void Start()
    {
        unit = GetComponent<Unit>();
    }

    public void Move(Vector2 velocity)
    {
        transform.Translate((velocity * unit.speed) * Time.deltaTime);
    }

}
