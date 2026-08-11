using UnityEngine;

public class BuffPickup : MonoBehaviour
{
    [SerializeField] private BuffSelectionManager buffSelectionManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        buffSelectionManager.OpenSelection();

        Destroy(gameObject);
    }
}