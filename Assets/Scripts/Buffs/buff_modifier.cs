using System;
using UnityEngine;

[Serializable]
public class BuffModifier
{
    [Header("Stat")]
    public StatType statType;

    [Header("Value")]
    public BuffValueType valueType;
    public BuffDisplayType displayType;

    public float minimumValue;
    public float maximumValue;
}