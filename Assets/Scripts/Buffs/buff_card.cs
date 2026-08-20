using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuffCard : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text buffName;
    [SerializeField] private TMP_Text buffDescription;
    [SerializeField] private Button selectButton;

    private BuffData currentBuff;

    private float rolledValue1;
    private float rolledValue2;


    public void Setup(
        BuffData buff,
        float value1,
        float value2
    )
    {
        currentBuff = buff;

        rolledValue1 = value1;
        rolledValue2 = value2;

        icon.sprite = buff.icon;

        buffName.text = buff.buffName;


        // Format Stat 1.
        string formattedValue1 = FormatValue(
            value1,
            buff.modifierMode1,
            buff.rollType1
        );


        // Format Stat 2.
        string formattedValue2 = FormatValue(
            value2,
            buff.modifierMode2,
            buff.rollType2
        );


        // Replace placeholders in the description.
        buffDescription.text = buff.description
            .Replace(
                "{value1}",
                formattedValue1
            )
            .Replace(
                "{value2}",
                formattedValue2
            );
    }


    private string FormatValue(
        float value,
        BuffModifierMode modifierMode,
        RollType rollType
    )
    {
        string sign = value >= 0f ? "+" : "";

        string formattedNumber;


        // Whole numbers should never display decimals.
        if (rollType == RollType.WholeNumber)
        {
            formattedNumber =
                Mathf.Abs(Mathf.RoundToInt(value)).ToString();
        }
        else
        {
            // Decimal values are limited to 2 decimal places.
            formattedNumber =
                Mathf.Abs(value).ToString("0.##");
        }


        // Percentage modifier.
        if (modifierMode == BuffModifierMode.Percentage)
        {
            formattedNumber =
                (Mathf.Abs(value) * 100f).ToString("0.##");
            
            return sign +
                   (value < 0f ? "-" : "") +
                   formattedNumber +
                   "%";
        }


        // Flat modifier.
        return sign +
               (value < 0f ? "-" : "") +
               formattedNumber;
    }


    public void OnSelected()
    {
        if (currentBuff == null)
            return;


        Debug.Log(
            $"Selected buff: {currentBuff.buffName} | " +
            $"Value 1: {FormatDebugValue(rolledValue1)} | " +
            $"Value 2: {FormatDebugValue(rolledValue2)}"
        );


        BuffSelectionManager.Instance.SelectBuff(
            currentBuff,
            rolledValue1,
            rolledValue2
        );
    }


    private string FormatDebugValue(float value)
    {
        if (value >= 0f)
        {
            return "+" + value.ToString("0.##");
        }

        return value.ToString("0.##");
    }
}