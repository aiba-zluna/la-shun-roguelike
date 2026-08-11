using UnityEngine;

public class BuffSelectionManager : MonoBehaviour
{
    public static BuffSelectionManager Instance { get; private set; }

    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private BuffCard buffCardPrefab;
    [SerializeField] private Transform cardContainer;

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
        BuffData[] availableBuffs = (BuffData[])basicBuffs.Clone();

        for (int i = 0; i < 3; i++)
        {
            int randomIndex = Random.Range(0, availableBuffs.Length);

            BuffData selectedBuff = availableBuffs[randomIndex];

            BuffCard card = Instantiate(buffCardPrefab, cardContainer);

            card.Setup(selectedBuff);

            availableBuffs[randomIndex] =
                availableBuffs[availableBuffs.Length - 1];

            System.Array.Resize(
                ref availableBuffs,
                availableBuffs.Length - 1
            );
        }
    }

    public void SelectBuff(BuffData selectedBuff)
    {
        Debug.Log("Buff selected: " + selectedBuff.buffName);

        ClearCards();

        selectionPanel.SetActive(false);

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