using UnityEngine;

public enum ConditionType
{
    LessThan,
    LessThanOrEqual,
    Equal,
    GreaterThan,
    GreaterThanOrEqual
}

[CreateAssetMenu(
    fileName = "New Conditional Buff",
    menuName = "Buffs/Conditional Buff"
)]
public class ConditionalBuffData : ScriptableObject
{
    [Header("Condition")]
    public StatType conditionStat;

    public ConditionType condition;

    public float conditionValue;


    [Header("Effect")]
    public StatType effectStat;

    public BuffModifierType modifierType;

    public BuffModifierMode modifierMode;

    public RollType rollType;

    public float minimumValue;

    public float maximumValue;


    [Header("Behavior")]
    public bool removeWhenConditionFails;
}