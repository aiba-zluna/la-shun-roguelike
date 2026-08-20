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
        if (basicBuffs == null || basicBuffs.Length == 0)
        {
            Debug.LogWarning("No basic buffs assigned!");
            return;
        }

        // Create a temporary copy so the same buff
        // cannot appear more than once.
        BuffData[] availableBuffs = (BuffData[])basicBuffs.Clone();

        int cardsToCreate = Mathf.Min(3, availableBuffs.Length);

        for (int i = 0; i < cardsToCreate; i++)
        {
            int randomIndex = Random.Range(
                0,
                availableBuffs.Length
            );

            BuffData selectedBuff = availableBuffs[randomIndex];

            // Roll Stat 1.
            float rolledValue1 = RollValue(
                selectedBuff.rollType1,
                selectedBuff.minimumValue1,
                selectedBuff.maximumValue1
            );

            // Convert Increase/Decrease into
            // a positive/negative value.
            rolledValue1 = ApplyModifierType(
                rolledValue1,
                selectedBuff.modifierType1
            );


            // Roll Stat 2 only for MixedStatAdjust.
            float rolledValue2 = 0f;

            if (selectedBuff.buffType == BuffType.MixedStatAdjust &&
                selectedBuff.statType2 != StatType.None)
            {
                rolledValue2 = RollValue(
                    selectedBuff.rollType2,
                    selectedBuff.minimumValue2,
                    selectedBuff.maximumValue2
                );

                rolledValue2 = ApplyModifierType(
                    rolledValue2,
                    selectedBuff.modifierType2
                );
            }


            Debug.Log(
                $"Creating card: {selectedBuff.buffName} | " +
                $"Value 1: {FormatDebugValue(rolledValue1)} | " +
                $"Value 2: {FormatDebugValue(rolledValue2)}"
            );


            BuffCard card = Instantiate(
                buffCardPrefab,
                cardContainer
            );

            // Send the exact rolled values to the card.
            card.Setup(
                selectedBuff,
                rolledValue1,
                rolledValue2
            );


            // Remove selected buff from temporary pool.
            availableBuffs[randomIndex] =
                availableBuffs[availableBuffs.Length - 1];

            System.Array.Resize(
                ref availableBuffs,
                availableBuffs.Length - 1
            );
        }
    }


    private float RollValue(
        RollType rollType,
        float minimumValue,
        float maximumValue
    )
    {
        float rolledValue;

        switch (rollType)
        {
            case RollType.WholeNumber:

                rolledValue = Random.Range(
                    Mathf.RoundToInt(minimumValue),
                    Mathf.RoundToInt(maximumValue) + 1
                );

                break;


            case RollType.Decimal:

                rolledValue = Random.Range(
                    minimumValue,
                    maximumValue
                );

                // Limit decimal rolls to 2 decimal places.
                rolledValue = Mathf.Round(
                    rolledValue * 100f
                ) / 100f;

                break;


            default:

                rolledValue = minimumValue;

                break;
        }

        return rolledValue;
    }


    private float ApplyModifierType(
        float value,
        BuffModifierType modifierType
    )
    {
        switch (modifierType)
        {
            case BuffModifierType.Increase:

                return Mathf.Abs(value);


            case BuffModifierType.Decrease:

                return -Mathf.Abs(value);


            default:

                return value;
        }
    }


    private string FormatDebugValue(float value)
    {
        if (value >= 0f)
        {
            return "+" + value.ToString("0.##");
        }

        return value.ToString("0.##");
    }


    public void SelectBuff(
        BuffData selectedBuff,
        float rolledValue1,
        float rolledValue2
    )
    {
        if (selectedBuff == null)
            return;

        Debug.Log(
            $"Buff selected: {selectedBuff.buffName} | " +
            $"Value 1: {FormatDebugValue(rolledValue1)} | " +
            $"Value 2: {FormatDebugValue(rolledValue2)}"
        );


        // Apply the EXACT values that were shown
        // on the selected card.
        buffApplier.ApplyBuff(
            selectedBuff,
            rolledValue1,
            rolledValue2
        );


        // Remove the three cards.
        ClearCards();


        // Close selection panel.
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