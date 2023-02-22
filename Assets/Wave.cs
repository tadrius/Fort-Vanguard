using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : ObjectPool {
    [SerializeField] int goldReward;
    [SerializeField] int pointReward;

    public int GoldReward{ get { return goldReward; }}
    public int PointReward{ get { return pointReward; }}

    List<GameObject> spawnedEnemies = new List<GameObject>(); // to keep track of what enemies have been spawned once already
    protected bool allSpawnedOnce = false;

    void Start() {
        StartCoroutine(SpawnEnemies());
    }

    void Update() {
        // deactivate wave if all objects are inactive and all objects have spawned at least once
        if (!ObjectsAreActive() && allSpawnedOnce) {
            gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnEnemies() {
        allSpawnedOnce = false;
        foreach (GameObject obj in objects) {
            obj.SetActive(true);
            spawnedEnemies.Add(obj);
            yield return new WaitForSeconds(spawnDelay);
        }
        allSpawnedOnce = true;
    }

    public int CountRemainingEnemies() {
        return poolSize - CountDestroyedEnemies();
    }

    int CountDestroyedEnemies() {
        int count = 0;
        foreach (GameObject enemy in spawnedEnemies) {
            if (!enemy.activeSelf) {
                count++;
            }
        }
        return count;
    }
}
