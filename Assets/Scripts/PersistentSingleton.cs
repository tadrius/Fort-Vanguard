using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingleton : MonoBehaviour
{
    void Awake()
    {
        // keep as a singleton (destroy this instance if there are 2 or more instances)
        if (2 <= FindObjectsOfType<Scoreboard>().Length) {
            Destroy(this.gameObject);
        } else {
            // if this is an existing singleton, keep it alive.
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
