using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Tooltip("The default faction which will determine the look of the enemy headquarters and units.")]
    [SerializeField] Faction enemyFaction;
    [Tooltip("The number of seconds between the end of the previous wave and the start of the next wave.")]
    [SerializeField] float waveDelay = 10f;
    [Tooltip("The first wave will start after the wave delay times the multiplier.")]
    [SerializeField] float startDelayMultipler = 3f;
    [Tooltip("A short time buffer, in seconds, between waves and their delays.")]
    [SerializeField] [Range(0f, 1f)] float transitionBuffer = 1f;
    [SerializeField] List<Wave> waves = new List<Wave>();
    [Tooltip("The location of the enemy headquarters, from which enemies will spawn.")]
    [SerializeField] Vector2Int headquartersCoordinates = new Vector2Int(10, 13);

    ScoreKeeper scoreKeeper;
    Bank bank;
    Game game;

    float startTimer;
    int currentWaveIndex = 0;
    ManagerState state = ManagerState.WaveIsReady;

    public Faction EnemyFaction { get { return enemyFaction; } }
    public List<Wave> Waves { get { return waves; }}
    public int CurrentWaveIndex { get { return currentWaveIndex; }}
    public ManagerState State { get { return state; }}
    public float StartTimer { get { return startTimer; }}
    public Vector2Int HeadquartersCoordinates { get { return headquartersCoordinates; } }

    public enum ManagerState{ WaveIsReady, WaveIsStarting, WaveIsRunning, WaveCompleted, NoMoreWaves, TransitioningWaves };

    void Awake() {
        GameObject playerObject = GameObject.FindGameObjectWithTag(Player.playerTag);
        scoreKeeper = playerObject.GetComponent<ScoreKeeper>(); 
        bank = playerObject.GetComponent<Bank>();
        
        game = FindObjectOfType<Game>();

        FactionSettings factionSettings = FindObjectOfType<FactionSettings>();
        if (factionSettings != null)
        {
            factionSettings.SetRandomEnemyFaction();
            enemyFaction = factionSettings.EnemyFaction;
        }
    }

    void Start() {
        foreach (Wave wave in waves) {
            wave.gameObject.SetActive(false);
        }
        ResetTimers();
        startTimer *= startDelayMultipler; // double the time before the first wave

        // create the enemy's headquarters visuals
        GridManager gridManager = FindObjectOfType<GridManager>();
        GameObject headquarters = GameObject.Instantiate(enemyFaction.EnemyHeadquarters, transform);
        headquarters.transform.localPosition = gridManager.GetPositionFromCoordinates(headquartersCoordinates);
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
