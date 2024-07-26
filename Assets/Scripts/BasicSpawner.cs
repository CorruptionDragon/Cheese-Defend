using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    public List<GameObject> prefabs;
    public List<Transform> spawnPoints;
    public float spawnInterval = 2f;

    public void StartSpawning()
    {
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        SpawnEnemy();
        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnDelay());
    }

    void SpawnEnemy()
    {
        int randomPrefabID = Random.Range(0,prefabs.Count);
        GameObject spawnedEnemy = Instantiate(prefabs[randomPrefabID]);
    }
}
