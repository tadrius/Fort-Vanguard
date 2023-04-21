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

    Player player;
    ScoreKeeper scoreKeeper;
    Bank bank;
    PlayerHealth playerHealth;
    Game game;
    
    float startTimer;
    int currentWaveIndex = 0;
    ManagerState state = ManagerState.WaveIsReady;

    public List<Wave> Waves { get { return waves; }}
    public int CurrentWaveIndex { get { return currentWaveIndex; }}
    public ManagerState State { get { return state; }}
    public float StartTimer { get { return startTimer; }}

    public enum ManagerState{ WaveIsReady, WaveIsStarting, WaveIsRunning, WaveCompleted, NoMoreWaves, TransitioningWaves };

    void Awake() {
        GameObject playerObject = GameObject.FindGameObjectWithTag(Player.playerTag);
        player = playerObject.GetComponent<Player>();
        scoreKeeper = playerObject.GetComponent<ScoreKeeper>(); 
        bank = playerObject.GetComponent<Bank>();
        playerHealth = playerObject.GetComponent<PlayerHealth>();
        
        game = FindObjectOfType<Game>();
    }

    void Start() {
        foreach (Wave wave in waves) {
            wave.gameObject.SetActive(false);
        }
        ResetTimers();
    }

    void Update() {
        switch (state) {
            case ManagerState.WaveIsReady:
                StartCoroutine(StartCurrentWave());
                break;
            // case ManagerState.WaveIsStarting:
            //     break;
            case ManagerState.WaveIsRunning:
                CheckForWaveCompletion();
                break;
            case ManagerState.WaveCompleted:
                StartCoroutine(TransitionWaves());
                break;
            // case ManagerState.TransitioningWaves:
            //     break;
            case ManagerState.NoMoreWaves:
                game.WinGame();
                break;
        }
    }

    IEnumerator StartCurrentWave() {
        state = ManagerState.WaveIsStarting;
        while (-transitionBuffer < startTimer) {
            startTimer -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        waves[currentWaveIndex].gameObject.SetActive(true);
        state = ManagerState.WaveIsRunning;
    }

    void CheckForWaveCompletion() {
        if (waves[currentWaveIndex].WaveCompleted) {
            state = ManagerState.WaveCompleted;
        }
    }

    IEnumerator TransitionWaves() {
        state = ManagerState.TransitioningWaves;
        yield return StartCoroutine(EndCurrentWave());
        PrepareNextWave();
    }

    IEnumerator EndCurrentWave() {
        yield return new WaitForSeconds(transitionBuffer);
        bank.Deposit(waves[currentWaveIndex].GoldReward); // deposit rewards
        scoreKeeper.AddToScore(waves[currentWaveIndex].PointReward);        
    }

    void PrepareNextWave() {
        if (currentWaveIndex + 1 < waves.Count) {
            currentWaveIndex++;
            ResetTimers();
            state = ManagerState.WaveIsReady;
        } else {
            state = ManagerState.NoMoreWaves;
        }
    }

    void ResetTimers() {
        startTimer = waveDelay; // reset start timer
    }

    public Wave GetCurrentWave() {
        return waves[currentWaveIndex];
    }

}
