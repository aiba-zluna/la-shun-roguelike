using UnityEngine;

public class BuffApplier : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;

    public void ApplyBuff(
        BuffData buff,
        float value1,
        float value2 = 0f
    )
    {
        if (buff == null)
            return;

        if (playerStats == null)
        {
            Debug.LogError(
                "BuffApplier: PlayerStats is not assigned!"
            );

            return;
        }

        switch (buff.buffType)
        {
            case BuffType.FlatStatAdjust:

                ApplyStatBuff(
                    buff.statType1,
                    value1,
                    buff.modifierMode1
                );

                Debug.Log(
                    $"Applied buff: {buff.buffName} | " +
                    $"Stat: {buff.statType1} | " +
                    $"Value: {FormatValue(value1, buff.modifierMode1)}"
                );

                break;


            case BuffType.MixedStatAdjust:

                ApplyStatBuff(
                    buff.statType1,
                    value1,
                    buff.modifierMode1
                );

                if (buff.statType2 != StatType.None)
                {
                    ApplyStatBuff(
                        buff.statType2,
                        value2,
                        buff.modifierMode2
                    );
                }

                Debug.Log(
                    $"Applied mixed buff: {buff.buffName} | " +
                    $"{buff.statType1}: " +
                    $"{FormatValue(value1, buff.modifierMode1)} | " +
                    $"{buff.statType2}: " +
                    $"{FormatValue(value2, buff.modifierMode2)}"
                );

                break;


            case BuffType.ConditionalStatAdjust:

                Debug.Log(
                    $"Conditional buff registered: " +
                    $"{buff.buffName}"
                );

                break;


            case BuffType.BulletEffect:

                Debug.Log(
                    $"Bullet effect buff not implemented yet: " +
                    $"{buff.buffName}"
                );

                break;


            case BuffType.SkillChance:

                Debug.Log(
                    $"Skill chance buff not implemented yet: " +
                    $"{buff.buffName}"
                );

                break;


            case BuffType.SkillDamage:

                Debug.Log(
                    $"Skill damage buff not implemented yet: " +
                    $"{buff.buffName}"
                );

                break;


            case BuffType.SpecialStatAdjust:

                Debug.Log(
                    $"Special stat buff not implemented yet: " +
                    $"{buff.buffName}"
                );

                break;
        }
    }


    private void ApplyStatBuff(
        StatType statType,
        float value,
        BuffModifierMode modifierMode
    )
    {
        switch (statType)
        {
            case StatType.MaxHealth:

                playerStats.maxHealth =
                    ApplyModifier(
                        playerStats.maxHealth,
                        value,
                        modifierMode
                    );

                break;


            case StatType.AttackDamage:

                playerStats.attackDamage =
                    ApplyModifier(
                        playerStats.attackDamage,
                        value,
                        modifierMode
                    );

                break;


            case StatType.AttackSpeed:

                playerStats.attackSpeed =
                    ApplyModifier(
                        playerStats.attackSpeed,
                        value,
                        modifierMode
                    );

                break;


            case StatType.BulletSpeed:

                playerStats.bulletSpeed =
                    ApplyModifier(
                        playerStats.bulletSpeed,
                        value,
                        modifierMode
                    );

                break;


            case StatType.MoveSpeed:

                playerStats.moveSpeed =
                    ApplyModifier(
                        playerStats.moveSpeed,
                        value,
                        modifierMode
                    );

                break;


            case StatType.Dash:

                playerStats.dash =
                    ApplyModifier(
                        playerStats.dash,
                        value,
                        modifierMode
                    );

                break;


            case StatType.None:

                Debug.LogWarning(
                    "BuffApplier: StatType is None."
                );

                break;
        }
    }


    private float ApplyModifier(
        float currentValue,
        float value,
        BuffModifierMode modifierMode
    )
    {
        switch (modifierMode)
        {
            case BuffModifierMode.Flat:

                return currentValue + value;


            case BuffModifierMode.Percentage:

                return currentValue + (currentValue * value);


            default:

                return currentValue;
        }
    }


    private string FormatValue(
        float value,
        BuffModifierMode modifierMode
    )
    {
        string sign = value >= 0f ? "+" : "";

        if (modifierMode == BuffModifierMode.Percentage)
        {
            return sign +
                   (value * 100f).ToString("0.##") +
                   "%";
        }

        return sign +
               value.ToString("0.##");
    }
}