using UnityEngine;

[RequireComponent(typeof(IUnit))]
public class UnitController : MonoBehaviour
{
    protected UnitProperties unitProperties;
    protected float screenWidth;
    protected float screenHeight;

    private void Start()
    {
        if (TryGetComponent(out IUnit unit))
        {
            unitProperties = unit.UnitProperties;
        }
        screenWidth = GameRules.screenWidth + unitProperties.widthPad;
        screenHeight = GameRules.screenHeight + unitProperties.heightPad;
    }

    public void Move(Vector3 velocity)
    {
        transform.Translate(velocity);

        if (transform.position.x >= screenWidth)
            transform.position = new Vector3(screenWidth, transform.position.y, 0);
        if (transform.position.x <= -screenWidth)
            transform.position = new Vector3(-screenWidth, transform.position.y, 0);
        if (transform.position.y >= screenHeight)
            transform.position = new Vector3(transform.position.x, screenHeight, 0);
        if (transform.position.y <= -screenHeight)
            transform.position = new Vector3(transform.position.x, -screenHeight, 0);
    }

    private void MoveEnemy(Vector3 velocity)
    {
        if (transform.position.x >= screenWidth ||
                    transform.position.x <= -screenWidth ||
                    transform.position.y >= screenHeight ||
                    transform.position.y <= -screenHeight )
            Destroy(gameObject);
    }
}
