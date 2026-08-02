using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100; //buffable
    public float currentHealth; //healable

    [Header("Weapon")]
    public float attackDamage = 2; //buffable
    public float attackSpeed = 1f; //buffable

    public float bulletSpeed = 10; //buffable
    public float bulletLifetime = 2;

    public int bulletsPerShot = 2; 
    public float shotDelay = 0.08f;

    [Header("Movement")]
    public float moveSpeed = 5; //buffable
    public float dash = 5;  //seconds //buffable

}