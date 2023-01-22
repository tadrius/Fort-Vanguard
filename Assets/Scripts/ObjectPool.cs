using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] [Range(0f, 10f)] float enemySpawnDelay = 1f;
    [SerializeField] List<GameObject> enemyTypes;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies() {
        while (true) {
            // spawn an enemy of a random type
            GameObject enemyType = enemyTypes[Random.Range(0, enemyTypes.Count - 1)];
            GameObject enemy = GameObject.Instantiate(enemyType, transform);
            yield return new WaitForSeconds(enemySpawnDelay);
        }
    }
}
