using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreAmountText;
    [SerializeField] TMP_Text scoreAmountText;
    [SerializeField] TMP_Text healthAmountText;
    [SerializeField] TMP_Text goldAmountText;
    [SerializeField] TMP_Text waveText;
    [SerializeField] TMP_Text countdownText;

    Scoreboard scoreboard;
    ScoreKeeper scoreKeeper;
    Bank bank;
    PlayerHealth playerHealth;
    WaveManager waveManager;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Player.playerTag);
        scoreKeeper = player.GetComponent<ScoreKeeper>(); 
        bank = player.GetComponent<Bank>();
        playerHealth = player.GetComponent<PlayerHealth>();
        
        waveManager = GameObject.FindObjectOfType<WaveManager>();
        scoreboard = GameObject.FindObjectOfType<Scoreboard>();
        UpdateHighScore();
    }

    void Update() {
        UpdateScore();
        UpdateHealth();
        UpdateGold();
        UpdateWave(); 
        UpdateCountdown();   
    }

    public void UpdateScore() {
        if (null != scoreKeeper) {
            scoreAmountText.text = $"{scoreKeeper.Score}";
        }
    }

    public void UpdateHighScore() {
        if (null != scoreboard) {
            highScoreAmountText.text = $"{scoreboard.GetHighScore()}";
        }
    }

    public void UpdateHealth() {
        if (null != playerHealth) {
            healthAmountText.text = $"{playerHealth.Health}";
        }
    }

    public void UpdateGold() {
        if (null != bank) {
            goldAmountText.text = $"{bank.CurrentBalance}";
        }
    }

    public void UpdateWave() {
        if (null != waveManager) {
            waveText.text = $"Wave {waveManager.CurrentWaveIndex + 1}:";
        }
    }

    public void UpdateCountdown() {
        int countdown = 0;
        if (null != waveManager) {
            if (waveManager.WaveIsRunning) { // show enemies remaining in wave
                countdown = waveManager.GetCurrentWave().CountRemainingEnemies();
            } else { // show seconds until wave start
                countdown = Mathf.CeilToInt(waveManager.StartTimer);
            }
            countdownText.text = $"{countdown}";
        }
    }
}
