using UnityEngine;

public static class GameRules
{

    // CAMERA
    public static Camera camMain;
    public static float screenHeight;
    public static float screenWidth;

    // COLLISION 
    public static float collisionDamageDelay = 1f;

    // Start is called before the first frame update
    static GameRules()
    {
        camMain = Camera.main;
        screenHeight = Screen.height;
        screenWidth = Screen.width;
        screenHeight = camMain.orthographicSize;
        screenWidth = (camMain.aspect * (screenHeight * 2f) / 2f);
    }


}
