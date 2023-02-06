using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeSpawns : MonoBehaviour
{
    readonly public static string runtimeSpawnsTag = "RuntimeSpawns";

    public void SpawnFX(GameObject fx, Vector3 position) {
        if (null != fx) {
            GameObject newFX = Instantiate(fx, position, Quaternion.identity);
            newFX.transform.parent = transform;     
        }
    }
}
