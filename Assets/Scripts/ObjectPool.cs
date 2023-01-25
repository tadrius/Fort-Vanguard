using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] int poolSize = 10;
    [SerializeField] [Range(0f, 10f)] float spawnDelay = 1f;
    [SerializeField] GameObject objectPrefab;

    GameObject[] objects;

    void Awake() {
        PopulatePool();
    }

    void Start()
    {
        StartCoroutine(SpawnObjects());
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

    IEnumerator SpawnObjects() {
        while (true) {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
