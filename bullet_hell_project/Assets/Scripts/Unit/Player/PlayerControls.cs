using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    IUnit unit;
    UnitController unitController;

    void Start()
    {
        unit = GetComponent<IUnit>();
        unitController = GetComponent<UnitController>();
    }

    void Update()
    {
        Vector3 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 velocity = direction * unit.UnitProperties.speed * Time.deltaTime;
        unitController.Move(velocity);
    }
}
