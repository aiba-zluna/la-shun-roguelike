using UnityEngine;

public enum BuffType
{
    FlatStatAdjust,
    MixedStatAdjust,
    BulletEffect,
    SkillChance,
    SkillDamage,
    SpecialStatAdjust,
    ConditionalStatAdjust
}

public enum StatType
{
    None,
    MaxHealth,
    AttackDamage,
    AttackSpeed,
    BulletSpeed,
    MoveSpeed,
    Dash
}

public enum BulletType
{
    None,
    HolyBullet,
    FreezingBullet,
    PiercingBullet,
    BouncingBullet,
    VampiricBullet
}

public enum RollType
{
    WholeNumber,
    Decimal
}

public enum BuffModifierType
{
    Increase,
    Decrease
}

public enum BuffModifierMode
{
    Flat,
    Percentage
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

    public StatType statType1;

    public StatType statType2;

    public BulletType bulletType;


    [Header("Stat 1 Value")]
    public BuffModifierType modifierType1;

    public BuffModifierMode modifierMode1;

    public RollType rollType1;

    public float minimumValue1;

    public float maximumValue1;


    [Header("Stat 2 Value")]
    public BuffModifierType modifierType2;

    public BuffModifierMode modifierMode2;

    public RollType rollType2;

    public float minimumValue2;

    public float maximumValue2;


    [Header("Conditional Buff")]
    public ConditionalBuffData conditionalBuff;


    [Header("Acquisition")]
    public bool oneTime;
}