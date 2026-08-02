using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100;
    public float currentHealth;

    [Header("Combat")]
    public float attackDamage = 10;
    public float defense = 5;
    public float attackSpeed = 1;

    [Header("Movement")]
    public float moveSpeed = 5;

}