using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior : ScriptableObject
{
    [Header("Spawn Point")]
    float spawnPointX;
    float spawnPointY;

    Vector3 startPoint;
    Vector3[] path;
    Vector3 endPoint;

}
