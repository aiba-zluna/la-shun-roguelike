using UnityEngine;

public class BuffSelectionManager : MonoBehaviour
{
    public static BuffSelectionManager Instance { get; private set; }

    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private BuffCard buffCardPrefab;
    [SerializeField] private Transform cardContainer;
    [SerializeField] private BuffApplier buffApplier;

    [Header("Basic Buffs")]
    [SerializeField] private BuffData[] basicBuffs;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenSelection()
    {
        Debug.Log("Buff selection opened!");

        selectionPanel.SetActive(true);

        CreateBuffCards();

        Time.timeScale = 0f;
    }

    private void CreateBuffCards()
    {
        // Make a temporary copy so we can remove buffs
        // from the pool and prevent duplicate cards.
        BuffData[] availableBuffs = (BuffData[])basicBuffs.Clone();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableBuffs.Length);

            BuffData selectedBuff = availableBuffs[randomIndex];

            // Roll the buff value ONCE.
            float rolledValue;

            if (selectedBuff.valueType == BuffValueType.Integer)
            {
                rolledValue = Random.Range(
                    Mathf.RoundToInt(selectedBuff.minimumValue),
                    Mathf.RoundToInt(selectedBuff.maximumValue) + 1
                );
            }
            else
            {
                rolledValue = Random.Range(
                    selectedBuff.minimumValue,
                    selectedBuff.maximumValue
                );
            }

            Debug.Log(
                "Creating card: " +
                selectedBuff.buffName +
                " | Rolled value: +" +
                rolledValue.ToString("0.##")
            );

            BuffCard card = Instantiate(
                buffCardPrefab,
                cardContainer
            );

            // Send both the buff and the rolled value
            // to the card.
            card.Setup(selectedBuff, rolledValue);

            // Remove the selected buff from the temporary pool
            // so another card cannot use the same buff.
            availableBuffs[randomIndex] =
                availableBuffs[availableBuffs.Length - 1];

            System.Array.Resize(
                ref availableBuffs,
                availableBuffs.Length - 1
            );
        }
    }

    public void SelectBuff(BuffData selectedBuff, float rolledValue)
    {
        Debug.Log(
            "Buff selected: " +
            selectedBuff.buffName +
            " | Value: +" +
            rolledValue.ToString("0.##")
        );

        // Apply the exact same value that was shown on the card.
        buffApplier.ApplyBuff(
            selectedBuff,
            rolledValue
        );

        // Remove the three cards.
        ClearCards();

        // Close the selection screen.
        selectionPanel.SetActive(false);

        // Resume the game.
        Time.timeScale = 1f;
    }

    private void ClearCards()
    {
        foreach (Transform child in cardContainer)
        {
            Destroy(child.gameObject);
        }
    }
}