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
    private float rolledValue;

    public void Setup(BuffData buff, float value)
    {
        currentBuff = buff;
        rolledValue = value;

        icon.sprite = buff.icon;
        buffName.text = buff.buffName;

        string formattedValue = FormatValue(buff, value);

        buffDescription.text = buff.description.Replace(
            "{value}",
            formattedValue
        );
    }

    private string FormatValue(BuffData buff, float value)
    {
        switch (buff.displayType)
        {
            case BuffDisplayType.Number:

                if (buff.valueType == BuffValueType.Integer)
                {
                    return "+" + Mathf.RoundToInt(value);
                }

                return "+" + value.ToString("0.##");

            case BuffDisplayType.Percentage:

            return "+" + (value * 100f).ToString("0.##") + "%";

            case BuffDisplayType.Seconds:

                return "+" + value.ToString("0.##") + "s";

            default:

                return "+" + value.ToString("0.##");
        }
    }

    public void OnSelected()
    {
        Debug.Log(
            "Selected buff: " +
            currentBuff.buffName +
            " | Value: +" +
            rolledValue.ToString("0.##")
        );

        BuffSelectionManager.Instance.SelectBuff(
            currentBuff,
            rolledValue
        );
    }
}