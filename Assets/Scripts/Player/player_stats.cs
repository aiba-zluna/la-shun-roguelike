using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100; //buffable STAT VAL ADJ
    public float currentHealth; //healable 

    [Header("Weapon")]
    public float attackDamage = 2; //buffable STAT VAL ADJ
    public float attackSpeed = 1f; //buffable STAT VAL ADJ

    public float bulletSpeed = 10; //buffable STAT VAL ADJ
    public float bulletLifetime = 2; //NOT BUFFABLE

    public int bulletsPerShot = 2; //NOT BUFFABLE - 2 bullet burst fire
    public float shotDelay = 0.08f; //NOT BUFFABLE - the gap between each bullet in burst fire

    [Header("Ultimate")]
    public bool ultimate = false; // bool - able to cast ultimate
    public float ultimateDamage = 10; //buffable SPECIAL VAL ADJ
    public float ultimateCooldown = 30; //buffable SPECIAL VAL ADJ
    public float ultimateFirerate = 0.1f; //NOT BUFFABLE
    public float ultimateSpeed = 10; //NOT BUFFABLE
    public float ultimateLifetime = 2; //NOT BUFFABLE
    public float ultimateDuration = 4; //buffable SPECIAL VAL ADJ 

    [Header("Movement")]
    public float moveSpeed = 5; //buffable STAT VAL ADJ
    public float dash = 5;  //cd in seconds & buffable STAT VAL ADJ

    [Header("Other Buffable Stats")]

    // BULLET EFFECTS
    public float lifesteal = 0; // % of attack damage buffable BULLET EFX
    public float pierce = 0; //bullets have smal aoe on impact buffable BULLET EFX
    public float slow = 0; //slow amount | negative values ex 0.20 buffable BULLET EFX

    // SKILL CHANCE AND DAMAGE
    public float chanceLaser = 0; //chance to spawn close ranged laser that pass through enemies
    public float chanceDiamond = 0; //chance to spawn huge aoe rounds
    public float chanceMissiles = 0; //chance to spawn enemy seeking missiles
    public float damageMissiles = 10; //missile rounds base damage
    public float damageLaser = 20; //laser rounds base damage
    public float damageDiamond = 30; //diamond rounds base damage

    // SPECIAL STATS
    public bool berserkStatus = false; // AS ~15% - 30% when HP < 50%
    public bool adrenalineStatus = false; // MS ~15% - 20% when HP < 30%
    public bool desperateStatus = false; // AD ~15% - 30% when HP < 50%
    public float reroll = 0; //amount of times to reroll a buff
    public float exodia = 0; //get 5 for instant win
}   