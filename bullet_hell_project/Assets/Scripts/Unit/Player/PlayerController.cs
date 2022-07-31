using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    protected UnitProperties unitProperties;
    protected float screenWidth;
    protected float screenHeight;

    private void Start()
    {
        if (TryGetComponent(out Player player))
        {
            unitProperties = player.UnitProperties;
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
}
