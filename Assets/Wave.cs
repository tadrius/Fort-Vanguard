using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : ObjectPool {
    [SerializeField] int goldReward;
    [SerializeField] int pointReward;

    public int GoldReward{ get { return goldReward; }}
    public int PointReward{ get { return pointReward; }}


    void Update() {
        // deactivate wave if all objects are inactive and all objects have spawned at least once
        if (!ObjectsAreActive() && allSpawnedOnce) {
            gameObject.SetActive(false);
        }
    }
}
