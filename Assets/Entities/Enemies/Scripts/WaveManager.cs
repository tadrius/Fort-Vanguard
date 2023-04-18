using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [Tooltip("The number of seconds between the end of the previous wave and the start of the next wave.")]
    [SerializeField] float waveDelay = 10f;
    [Tooltip("A short time buffer, in seconds, between waves and their delays.")]
    [SerializeField] [Range(0f, 1f)] float transitionBuffer = 1f;
    [SerializeField] List<Wave> waves = new List<Wave>();

    float startTimer;
    float endTimer;
    int currentWaveIndex = 0;
    bool waveIsRunning = false;
    bool noMoreWaves = false;

    public int CurrentWaveIndex { get { return currentWaveIndex; } }
    public bool WaveIsRunning { get { return waveIsRunning; } }
    public float StartTimer { get { return startTimer; } }

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
    }

    void Start() {
        foreach (Wave wave in waves) {
            wave.gameObject.SetActive(false);
        }
        ResetTimers();
    }

    void Update() {
        if (!waveIsRunning) {
            if (noMoreWaves) {
                player.WinGame();
            } else {
                StartCurrentWave();               
            }
        } else {
            if (waves[currentWaveIndex].AllSpawned // End current wave if the contained enemies have all spawned
                && waves[currentWaveIndex].WaveCompleted) { // And the wave is completed
                EndCurrentWave();
            }
        }      
    }

    void StartCurrentWave() {
        if (-transitionBuffer < startTimer) { // start delay with buffer
            startTimer -= Time.deltaTime;
            return;
        }
        waveIsRunning = true;
        waves[currentWaveIndex].gameObject.SetActive(true);
    }

    void EndCurrentWave() {
        if (-transitionBuffer < endTimer) { // end delay with buffer
            endTimer -= Time.deltaTime;
            return;
        }
        bank.Deposit(waves[currentWaveIndex].GoldReward); // deposit rewards
        scoreKeeper.AddToScore(waves[currentWaveIndex].PointReward);        
        waveIsRunning = false;
        PrepareNextWave();
    }

    void PrepareNextWave() {
        if (currentWaveIndex + 1 < waves.Count) {
            currentWaveIndex++;
            ResetTimers();
        } else {
            noMoreWaves = true;
        }
    }

    void ResetTimers() {
        startTimer = waveDelay; // reset start timer
        endTimer = 0f; // reset end timer       
    }

    public Wave GetCurrentWave() {
        return waves[currentWaveIndex];
    }

}
