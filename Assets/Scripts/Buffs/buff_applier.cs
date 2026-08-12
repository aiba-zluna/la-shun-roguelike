using UnityEngine;

public class BuffApplier : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    public void ApplyBuff(BuffData buff, float value)
    {
        if (buff == null)
            return;

        if (buff.buffType == BuffType.StatAdjust)
        {
            ApplyStatBuff(buff, value);
        }
    }

    private void ApplyStatBuff(BuffData buff, float value)
    {
        switch (buff.statType)
        {
            case StatType.AttackDamage:
                playerStats.attackDamage += value;
                break;

            case StatType.MaxHealth:
                playerStats.maxHealth += value;
                break;

            case StatType.AttackSpeed:
                playerStats.attackSpeed += value;
                break;

            case StatType.BulletSpeed:
                playerStats.bulletSpeed += value;
                break;

            case StatType.MoveSpeed:
                playerStats.moveSpeed += value;
                break;

            case StatType.Dash:
                playerStats.dash += value;
                break;
        }

        Debug.Log(
            $"Applied buff: {buff.buffName} | " +
            $"Stat: {buff.statType} | " +
            $"Value: +{value:0.##}"
        );
    }
}