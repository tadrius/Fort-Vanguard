using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : ObjectPool {
    [SerializeField] int goldReward;
    [SerializeField] int pointReward;

    public int GoldReward{ get { return goldReward; }}
    public int PointReward{ get { return pointReward; }}

    List<Unit> spawnedEnemies = new List<Unit>(); // to keep track of what enemies have been spawned once already
    bool allSpawned = false;
    bool waveCompleted = false;
    public bool AllSpawned { get { return allSpawned; }}
    public bool WaveCompleted { get { return waveCompleted; }}

    public void Start() {
        StartCoroutine(SpawnEnemies());
    }

    void Update() {
        // mark wave completed if the number of remaining enemies is 0
        if (0 == CountRemainingEnemies()) {
            waveCompleted = true;
        }
        // deactivate wave if all objects have spawned and all objects are inactive (this is done after marking the wave is completed to allow enemy corpses)
        if (allSpawned && !ObjectsAreActive()) {
            gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnEnemies() {
        allSpawned = false;
        foreach (GameObject obj in objects) {
            obj.SetActive(true);
            spawnedEnemies.Add(obj.GetComponentInChildren<Unit>());
            yield return new WaitForSeconds(spawnDelay);
        }
        allSpawned = true;
    }

    public int CountRemainingEnemies() {
        return poolSize - CountDestroyedEnemies();
    }

    int CountDestroyedEnemies() {
        int count = 0;
        foreach (Unit enemy in spawnedEnemies) {
            if (!enemy.gameObject.activeSelf) {
                count++;
            }
        }
        return count;
    }
}
