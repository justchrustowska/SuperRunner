using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawnSystem : MonoBehaviour
{
    public ChunkListConfig _chunkListConfig;

    public Transform spawnPoint; 

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject randomPrefab = GetRandomPrefab();
            if (randomPrefab != null)
            {
                Instantiate(randomPrefab, spawnPoint.position, Quaternion.identity);
                Debug.Log($"Spawned {randomPrefab.name}");
            }
        }
    }

    GameObject GetRandomPrefab()
    {
        if (_chunkListConfig.chunkList.Count == 0)
        {
            Debug.LogWarning("No prefabs available in the list.");
            return null;
        }

        int randomIndex = UnityEngine.Random.Range(0, _chunkListConfig.chunkList.Count);
        return _chunkListConfig.chunkList[randomIndex];
    }
}
