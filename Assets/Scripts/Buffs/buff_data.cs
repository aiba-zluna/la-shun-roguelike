using UnityEngine;

public enum BuffType
{
    StatAdjust
}

public enum StatType
{
    MaxHealth,
    AttackDamage,
    AttackSpeed,
    BulletSpeed,
    MoveSpeed,
    Dash
}

public enum BuffValueType
{
    Integer,
    Float
}

public enum BuffDisplayType
{
    Number,
    Percentage,
    Seconds
}

[CreateAssetMenu(fileName = "New Buff", menuName = "Buffs/Buff")]
public class BuffData : ScriptableObject
{
    [Header("Display")]
    public string buffName;

    [TextArea]
    public string description;

    public Sprite icon;

    [Header("Buff")]
    public BuffType buffType;
    public StatType statType;

    [Header("Value")]
    public BuffValueType valueType;
    public BuffDisplayType displayType;

    public float minimumValue;
    public float maximumValue;

    [Header("Acquisition")]
    public bool oneTime;
}