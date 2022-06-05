using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class ClampUnit : MonoBehaviour
{


    public float heightPad;
    public float widthPad;

    string unitTag;
    float screenHeight;
    float screenWidth;

    void Start()
    {
        unitTag = GetComponent<Unit>().GetUnitTag();
        screenWidth = GameRules.screenWidth + heightPad;
        screenHeight = GameRules.screenHeight + widthPad;
    }

    void Update()
    {
        Clamp();
    }

    void Clamp()
    {
        if (unitTag == "Player") 
        {

            // PLAYER
            if (transform.position.x >= screenWidth)
            {
                transform.position = new Vector3(screenWidth, transform.position.y, 0);
            }

            if (transform.position.x <= -screenWidth)
            {
                transform.position = new Vector3(-screenWidth, transform.position.y, 0);
            }

            if (transform.position.y >= screenHeight)
            {
                transform.position = new Vector3(transform.position.x, screenHeight, 0);
            }

            if (transform.position.y <= -screenHeight)
            {
                transform.position = new Vector3(transform.position.x, -screenHeight, 0);
            }
        }

        if (unitTag == "Enemy") 
        {
            // ENEMY
            if (transform.position.x >= screenWidth + widthPad || transform.position.x <= -screenWidth - widthPad ||
                transform.position.y >= screenHeight + heightPad || transform.position.y <= -screenHeight - heightPad)
            {
                
                Destroy(gameObject);
            }
        }
        
        

        
    }
}
