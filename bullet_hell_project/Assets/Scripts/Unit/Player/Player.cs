using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour, IUnit
{
    public static float playerHealth;

    [SerializeField]
    private UnitProperties unitProperties;
    public UnitProperties UnitProperties { get; set; }
    public PlayerController playerController;
    //
    protected float currentHealth;
    protected bool isDead = false;
    //
    public static List<Player> player = new List<Player>();

    void Awake()
    {
        UnitProperties = unitProperties;
        currentHealth = UnitProperties.healthMax;
        player.Add(this);
    }

    void Update()
    {
        if (currentHealth <= 0.0f && !isDead)
        {
            isDead = true;
            Destroy(gameObject);
        }

        Vector3 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        Vector3 velocity = direction * unitProperties.speed * Time.deltaTime;
        playerController.Move(velocity);
    }

    public void TakeDamage(float damage)
    {
        if (UnitProperties.isDestructible) currentHealth -= damage;
    }


}
