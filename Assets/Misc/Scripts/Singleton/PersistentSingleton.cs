using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SingletonNamer))]
public class PersistentSingleton : MonoBehaviour
{

    [SerializeField] int id = 0;

    public int Id { get { return id; }}

    void Awake()
    {
        int matchingSingletons = CountMatchingSingletons();

        // destroy this instance if there are 2 or more instances
        if (2 <= matchingSingletons) {
            Destroy(this.gameObject);
        } else {
            // otherwise, keep this alive
            DontDestroyOnLoad(this.gameObject);
        }
    }

    int CountMatchingSingletons() {
        // count the number of singletons with the same id as this
        int matchingSingletons = 0;
        PersistentSingleton[] singletons = FindObjectsOfType<PersistentSingleton>();
        foreach (PersistentSingleton singleton in singletons) {
            if (singleton.id == id) {
                matchingSingletons++;
            }
        }
        return matchingSingletons;
    }
    
}
