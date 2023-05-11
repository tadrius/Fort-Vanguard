using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{

    [SerializeField] [Range(0, 200)] int poolSize = 50;
    [SerializeField] protected GameObject objectPrefab;

    GameObject[] objects;

    public int PoolSize { get { return poolSize; }}

    void Awake() {
        PopulatePool();
    }

    protected void PopulatePool() {
        objects = new GameObject[poolSize];
        for (int i = 0; i < objects.Length; i++) {
            objects[i] = GameObject.Instantiate(objectPrefab, transform);
            objects[i].SetActive(false);
            objects[i].name = $"{objectPrefab.name} {i}";
        }
    }

    // Enable any object in the pool. Return a boolean indicating success.
    public GameObject EnableObject() {
        foreach (GameObject obj in objects) {
            if (!obj.activeSelf) {
                obj.SetActive(true);
                return obj; // object enabled
            }
        }
        return null; // no objects to enable
    }

    // Enable the object at the given index and return it. If the obj 
    public GameObject EnableObject(int index) {
        if (index < poolSize) {
            GameObject obj = objects[index];
            if (!obj.activeSelf) {
                obj.SetActive(true);
                return obj;
            }
        }
        return null; // index is out-of-bounds or object is already enabled
    }

    // Returns true if any object in the pool is active.
    public bool ObjectsAreActive() {
        foreach (GameObject obj in objects) {
            if (null != obj && obj.activeSelf) {
                return true;
            }
        }
        return false;
    }

}
