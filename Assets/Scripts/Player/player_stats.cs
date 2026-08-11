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
    public float bulletLifetime = 2; //no change

    public int bulletsPerShot = 2; //no change - 2 bullet burst fire
    public float shotDelay = 0.08f; //no change - the gap between each bullet in burst fire

    [Header("Movement")]
    public float moveSpeed = 5; //buffable
    public float dash = 5;  //cd in seconds & buffable

    [Header("Buffs")]
    public float lifesteal = 0; // % of attack damage
    public float dot = 0; // flat dot (sec)
    public float pierce = 0; //bullets have smal aoe on impact
    public float slow = 0; //slow amount | negative values ex 0.20
    public float reroll = 0; //amount of times to reroll a buff
    public bool ultimate = false; // bool - able to cast ultimate
    public float chanceLaser = 0; //chance to spawn close ranged laser that pass through enemies
    public float chanceDiamond = 0; //chance to spawn huge aoe rounds
    public float chanceMissiles = 0; //chance to spawn enemy seeking missiles
    public float damageMissiles = 10; //missile rounds base damage
    public float damageLaser = 20; //laser rounds base damage
    public float damageDiamond = 30; //diamond rounds base damage
    public float damageUltimate = 40; //ultimate damage
    public float cooldownUltimate = 30; //Ultimate cooldown base 30s
    public bool berserkStatus = false; // AS ~15% - 30% when HP < 50%
    public bool adrenalineStatus = false; // MS ~15% - 20% when HP < 30%
    public bool desperateStatus = false; // AD ~15% - 30% when HP < 50%
}   