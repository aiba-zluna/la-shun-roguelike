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

    public void Setup(BuffData buff)
    {
        currentBuff = buff;

        icon.sprite = buff.icon;
        buffName.text = buff.buffName;
        buffDescription.text = buff.description;
    }

    public void OnSelected()
    {
        Debug.Log("Selected buff: " + currentBuff.buffName);

        BuffSelectionManager.Instance.SelectBuff(currentBuff);
    }
}