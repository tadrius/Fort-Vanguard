using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [Tooltip("The number of seconds between the end of the previous wave and the start of the next wave.")]
    [SerializeField] float waveDelay = 10f;
    [Tooltip("A short time buffer, in seconds, waves and their delays.")]
    [SerializeField] [Range(0f, 1f)] float transitionBuffer = 1f;
    [SerializeField] List<Wave> waves = new List<Wave>();

    float startTimer;
    float endTimer;
    int currentWaveIndex = 0;
    bool waveIsRunning = false;

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

        foreach (Wave wave in waves) {
            wave.gameObject.SetActive(false);
        }
    }

    void Start() {
        startTimer = waveDelay;
        endTimer = 0f;
    }

    void Update() {
        if (!waveIsRunning) { // No current wave.
            if (currentWaveIndex < waves.Count) { // More waves to go.
                StartCurrentWave();
            } else { // No additional waves.
                player.WinGame();
            }
        } else { // Current wave is set.
            if (false == waves[currentWaveIndex].gameObject.activeSelf) { // Current wave was deactivated.
                EndCurrentWave();
            }
        }      
    }

    void StartCurrentWave() {
        if (-transitionBuffer < startTimer) { // start delay with buffer
            startTimer -= Time.deltaTime;
            return;
        }
        Debug.Log("Wave starting.");
        waves[currentWaveIndex].gameObject.SetActive(true); // Spawn wave
        waveIsRunning = true;
    }

    void EndCurrentWave() {
        if (-transitionBuffer < endTimer) { // end delay with buffer
            endTimer -= Time.deltaTime;
            return;
        }
        Debug.Log("Wave completed.");
        bank.Deposit(waves[currentWaveIndex].GoldReward); // deposit rewards
        scoreKeeper.AddToScore(waves[currentWaveIndex].PointReward);        
        waveIsRunning = false; // flag as finished running
        PrepareNextWave();
    }

    void PrepareNextWave() {
        currentWaveIndex++; // increment wave index
        startTimer = waveDelay; // reset wave delay
        endTimer = 0f;
    }

    public Wave GetCurrentWave() {
        return waves[currentWaveIndex];
    }

}
