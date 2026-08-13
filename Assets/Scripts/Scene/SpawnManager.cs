using UnityEngine;
using Pathfinding;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int enemyCount = 5;

    [Header("Spawn Area")]
    [SerializeField] private BoxCollider2D spawnArea;

    [Header("Player")]
    [SerializeField] private Transform player;
    [SerializeField] private float minimumPlayerDistance = 3f;

    private bool roomActivated;

    public void ActivateRoom()
    {
        if (roomActivated)
            return;

        roomActivated = true;
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPosition = GetRandomSpawnPosition();

            GameObject enemyPrefab =
                enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            Instantiate(
                enemyPrefab,
                spawnPosition,
                Quaternion.identity
            );
        }
    }

    Vector2 GetRandomSpawnPosition()
    {
        Bounds bounds = spawnArea.bounds;

        for (int i = 0; i < 20; i++)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            Vector2 position = new Vector2(x, y);

            NNInfo nodeInfo = AstarPath.active.GetNearest(position);

            if (nodeInfo.node != null && nodeInfo.node.Walkable)
            {
                return position;
            }
        }

        Debug.LogWarning("Could not find a walkable spawn position!");
        return transform.position;
    }
}
