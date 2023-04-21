using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    [Tooltip("The gold reward for completing the wave.")]
    [SerializeField] int goldReward;
    [Tooltip("The point reward for completing the wave.")]
    [SerializeField] int pointReward;
    [Tooltip("The number of times each pool in the spawn order will attempt to spawn a unit.")]
    [SerializeField] int spawnCycles;
    [Tooltip("The second delay between each unit is spawned.")]
    [SerializeField] float spawnDelay = 1f;
    [Tooltip("During a single spawn cycle, each pool in the order will attempt to spawn a unit, one after the other.")]
    [SerializeField] List<ObjectPool> spawnOrder;

    List<Unit> spawnedUnits = new List<Unit>();
    bool allSpawned;
    bool waveCompleted;
    int waveSize; // the total number of units in the wave

    public int GoldReward{ get { return goldReward; }}
    public int PointReward{ get { return pointReward; }}
    public List<Unit> SpawnedUnits { get { return spawnedUnits; }}
    public bool AllSpawned { get { return allSpawned; }}
    public bool WaveCompleted { get { return waveCompleted; }}

    void OnEnable() {
        waveSize = spawnCycles * spawnOrder.Count;
        spawnedUnits = new List<Unit>();
        waveCompleted = false;
        allSpawned = false;
        StartCoroutine(SpawnUnits());
    }

    void Update() {
        // mark wave completed if the number of remaining enemies is 0
        if (0 == CountRemainingEnemies()) {
            waveCompleted = true;
        }
        // deactivate wave if all objects have spawned and all objects are inactive (this is done after marking the wave is completed to allow enemy corpses)
        if (allSpawned && !UnitsAreActive()) {
            gameObject.SetActive(false);
        }
    }

    IEnumerator SpawnUnits() {
        for (int i = 0; i < spawnCycles; i++) {
            foreach (ObjectPool pool in spawnOrder) {
                GameObject unitObj = pool.EnableObject();
                if (null != unitObj) {
                    Unit unit = unitObj.GetComponent<Unit>();
                    unit.enabled = true;
                    spawnedUnits.Add(unit);
                }
            yield return new WaitForSeconds(spawnDelay);
            }
        }
        allSpawned = true;
    }

    public int CountRemainingEnemies() {
        return waveSize - CountDestroyedEnemies();
    }

    int CountDestroyedEnemies() {
        int count = 0;
        foreach (Unit unit in spawnedUnits) {
            if (!unit.isActiveAndEnabled) {
                count++;
            }
        }
        return count;
    }

    // Returns true if there are any active units in the wave
    bool UnitsAreActive() {
        foreach (Unit unit in spawnedUnits) {
            if (unit.isActiveAndEnabled) {
                return true;
            }
        }
        return false;
    }
}
