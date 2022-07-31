using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [Header("Missile Art")]
    public Projectile normalMissile;
    public Projectile hope;
    public Projectile despair;

    [Space]
    [Header("Weapon Properties")]
    public float damage = 1;
    public string damageTag;
    public float maxRange;
    public bool isPiercing = false;
    public bool isPolarized = false;
    public float movementDelay = 0.0f;

    [Space]
    [Header("Weapon Specs")]
    public int primaryTurretCount;
    public int secondaryTurretCount;

    [Space]
    [Header("Initial Direction")]
    public float startInitialAngle;
    public float endInitialAngle;

    [Space]
    [Header("Path")]
    public float[] curveAngle;

    [Space]
    [Header("FInal Direction")]
    public float startFinalAngle;
    public float endFinalAngle;

    [Space]
    [Header("Secondary Spread")]
    public float startSpread;
    public float endSpread;

    [Space]
    [Header("Speed")]
    public float startSpeed;
    public float endSpeed;

    [Space]
    [Header("Delay")]
    public float initialDelay;
    public float primaryRateOfFire;
    public float secondaryRateOfFire;

    [Space]
    [Header("Offset")]
    public float offsetX;
    public float offsetY;

    [Space]
    [Header("Weapon Modes")]
    public bool isInverted = false;
    public bool isLocking = false;
    public bool isCycling = false;

    [Space]
    [Header("Delay")]
    public float homingDelay = 0.0f;
    public float homingInterval = 0.0f;
    public float homingDistance = 1.0f;

    
    
}
