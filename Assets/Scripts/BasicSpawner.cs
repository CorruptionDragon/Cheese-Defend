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

    [SerializeField]
    public Transform[] waypoints;

    public Pathfinding script;

    private void Start()
    {
        StartCoroutine(spawnEnemy(basicroachInterval, basicroachPrefab));
        StartCoroutine(spawnEnemy(fastroachInterval, fastroachPrefab));
        StartCoroutine(spawnEnemy(tankroachInterval, tankroachPrefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(-5f, 5f), Random.Range(-6f, 6f), 0), Quaternion.identity);
        script = newEnemy.GetComponent<Pathfinding>();

        if (script != null)
        {
            script.waypoints = waypoints;
        }

        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
