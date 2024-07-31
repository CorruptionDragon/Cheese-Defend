using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicSpawner : MonoBehaviour
{
    [SerializeField] private GameObject basicroachPrefab;
    [SerializeField] private GameObject fastroachPrefab;
    [SerializeField] private GameObject tankroachPrefab;

    [SerializeField] private float basicroachInterval = 3f;
    [SerializeField] private float fastroachInterval = 3f;
    [SerializeField] private float tankroachInterval = 3f;

    private void Start()
    {
        StartCoroutine(spawnEnemy())
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
