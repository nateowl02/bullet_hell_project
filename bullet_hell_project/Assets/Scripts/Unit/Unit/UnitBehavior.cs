using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehavior : ScriptableObject
{
    [Header("Start")]
    public Vector3 start;

    [Header("Paths")]
    public Vector3[] paths;

    [Header("End")]
    public Vector3 end;
    
    [Space]
    [Header("Spawn")]
    public Vector3 spawn;
}
