using UnityEngine;

[CreateAssetMenu(fileName = "New Buff", menuName = "Buffs/Buff")]
public class BuffData : ScriptableObject
{
    [Header("Display")]
    public string buffName;
    [TextArea]
    public string description;
    public Sprite icon;
}