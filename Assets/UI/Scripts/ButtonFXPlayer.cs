using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFXPlayer : MonoBehaviour
{

    [SerializeField] GameObject fx;

    public void PlayButtonFX() {
        RuntimeSpawns runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag)
            .GetComponent<RuntimeSpawns>();
        runtimeSpawns.SpawnObject(fx, Vector3.zero);
    }
}
