using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class UnitProperties : ScriptableObject
{
    [Header("Basic Stat")]
    public float healthMax;
    public float speed;

    [Header("Collision")]
    public float collisionDamage;
    public string collisionDamageTag;
    public bool isDestructible;

    [Header("Clamping")]
    public float heightPad;
    public float widthPad;
}
