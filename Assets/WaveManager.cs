using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField] float waveDelay = 10f;
    [SerializeField] List<Wave> waves = new List<Wave>();

    float delayTimer;
    int currentWaveIndex = 0;
    bool waveIsRunning = false;

    Player player;
    ScoreKeeper scoreKeeper;
    Bank bank;
    PlayerHealth playerHealth;

    void Awake() {
        GameObject playerObject = GameObject.FindGameObjectWithTag(Player.playerTag);
        player = playerObject.GetComponent<Player>();
        scoreKeeper = playerObject.GetComponent<ScoreKeeper>(); 
        bank = playerObject.GetComponent<Bank>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();

        foreach (Wave wave in waves) {
            wave.gameObject.SetActive(false);
        }
    }

    void Start() {
        delayTimer = waveDelay;
    }

    void Update() {
        if (!waveIsRunning) { // No current wave.
            if (currentWaveIndex < waves.Count) { // More waves to go.
                SpawnWave();
            } else { // No additional waves.
                player.ExecuteGameOverSequence(); // Victory.
            }
        } else { // Current wave is set.
            if (false == waves[currentWaveIndex].gameObject.activeSelf) { // Current wave was deactivated.
                EndCurrentWave();
                PrepareNextWave();
            }
        }      
    }

    void SpawnWave() {
        if (delayTimer == waveDelay) {
            Debug.Log($"Wave starting in {delayTimer} seconds.");
        }
        if (0 < delayTimer) { // Countdown
            delayTimer -= Time.deltaTime;
            return;
        }
        Debug.Log("Wave starting.");
        waves[currentWaveIndex].gameObject.SetActive(true); // Spawn wave
        waveIsRunning = true;  
    }

    void EndCurrentWave() {
        Debug.Log("Wave completed.");
        bank.Deposit(waves[currentWaveIndex].GoldReward); // deposit rewards
        scoreKeeper.AddToScore(waves[currentWaveIndex].PointReward);        
        waveIsRunning = false; // flag as finished running
    }

    void PrepareNextWave() {
        currentWaveIndex++; // increment wave index
        delayTimer = waveDelay; // reset wave delay             
    }

}
