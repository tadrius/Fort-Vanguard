using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] [Range(0, 200)] int poolSize = 50;
    [SerializeField] [Range(0.05f, 20f)] float spawnDelay = 1.75f;
    [SerializeField] GameObject objectPrefab;

    GameObject[] objects;
    protected bool allSpawnedOnce = false;

    void Awake() {
        PopulatePool();
    }

    void Start() {
        StartCoroutine(SpawnObjectsOnce());
    }

    void PopulatePool() {
        objects = new GameObject[poolSize];
        for (int i = 0; i < objects.Length; i++) {
            objects[i] = GameObject.Instantiate(objectPrefab, transform);
            objects[i].SetActive(false);
            objects[i].name = $"{objectPrefab.name} {i}";
        }
    }

    void EnableObjectInPool() {
        foreach (GameObject obj in objects) {
            if (!obj.activeSelf) {
                obj.SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnObjectsContinuously() {
        while (true) {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    IEnumerator SpawnObjectsOnce() {
        allSpawnedOnce = false;
        foreach (GameObject obj in objects) {
            obj.SetActive(true);
            yield return new WaitForSeconds(spawnDelay);
        }
        allSpawnedOnce = true;
    }

    public bool ObjectsAreActive() {
        foreach (GameObject enemyObject in objects) {
            if (enemyObject.activeSelf) {
                return true;
            }
        }
        return false;
    }

}
