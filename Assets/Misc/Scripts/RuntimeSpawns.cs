using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSpawns : MonoBehaviour
{
    readonly public static string runtimeSpawnsTag = "RuntimeSpawns";

    public GameObject SpawnObject(GameObject gameObjectPrefab, Vector3 position) {
        if (null != gameObjectPrefab) {
            GameObject newGameObject = Instantiate(gameObjectPrefab, position, Quaternion.identity);
            newGameObject.transform.parent = transform;
            return newGameObject;
        }
        return null;
    }
}
