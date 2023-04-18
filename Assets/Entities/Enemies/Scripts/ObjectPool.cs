using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] [Range(0, 200)] protected int poolSize = 50;
    [SerializeField] [Range(0.05f, 20f)] protected float spawnDelay = 1.75f;
    [SerializeField] GameObject objectPrefab;

    protected GameObject[] objects;

    void Awake() {
        PopulatePool();
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

    public bool ObjectsAreActive() {
        foreach (GameObject poolObject in objects) {
            if (null != poolObject && poolObject.activeSelf) { // null check to handle destroyed objects
                return true;
            }
        }
        return false;
    }

}
