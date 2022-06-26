using UnityEngine;

[RequireComponent(typeof(Unit))]
public class ClampUnit : MonoBehaviour
{

    public float heightPad;
    public float widthPad;
    //
    string unitTag;
    float screenHeight;
    float screenWidth;
    //

    void Start()
    {
        unitTag = GetComponent<Unit>().GetUnitTag();
        screenWidth = GameRules.screenWidth + widthPad;
        screenHeight = GameRules.screenHeight + heightPad;
    }

    void Update()
    {
        Clamp();
    }

    void Clamp()
    {
        if (unitTag == "Player") 
        {
            if (transform.position.x >= screenWidth)
                transform.position = new Vector3(screenWidth, transform.position.y, 0);
            if (transform.position.x <= -screenWidth)
                transform.position = new Vector3(-screenWidth, transform.position.y, 0);
            if (transform.position.y >= screenHeight)
                transform.position = new Vector3(transform.position.x, screenHeight, 0);
            if (transform.position.y <= -screenHeight)
                transform.position = new Vector3(transform.position.x, -screenHeight, 0);
        }

        if (unitTag == "Enemy") 
        {
            if (transform.position.x >= screenWidth + widthPad || 
                transform.position.x <= -screenWidth - widthPad ||
                transform.position.y >= screenHeight + heightPad || 
                transform.position.y <= -screenHeight - heightPad)
                Destroy(gameObject);
        } 
    }
}
