using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class SingletonNamer : MonoBehaviour
{
    [SerializeField] string baseName;

    PersistentSingleton persistentSingleton;

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) {
            UpdateObjectName();
        }
    }

    void UpdateObjectName() {
        int singletonId = GetComponent<PersistentSingleton>().Id;
        transform.name = $"{baseName} singleton{singletonId}";
    }
}
