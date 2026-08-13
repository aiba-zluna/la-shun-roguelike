using UnityEngine;

public class spawnTrigger : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER ENTERED DOOR!");

            spawnManager.ActivateRoom();
        }
    }
}
