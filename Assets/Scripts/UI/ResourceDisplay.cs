using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text moneyText;

    Scoreboard scoreboard;
    ScoreKeeper scoreKeeper;
    Bank bank;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Player.playerTag);
        scoreKeeper = player.GetComponent<ScoreKeeper>(); 
        bank = player.GetComponent<Bank>();
        playerHealth = player.GetComponent<PlayerHealth>();
        
        scoreboard = GameObject.FindObjectOfType<Scoreboard>();
    }

    void Update() {
        UpdateScore();
        UpdateHighScore();
        UpdateHealth();
        UpdateMoney();        
    }

    public void UpdateScore() {
        if (null != scoreKeeper) {
            scoreText.text = $"Score: {scoreKeeper.Score}";
        }
    }

    public void UpdateHighScore() {
        if (null != scoreboard) {
            highScoreText.text = $"High Score: {scoreboard.GetHighScore().Key}";
        }
    }

    public void UpdateHealth() {
        if (null != playerHealth) {
            healthText.text = $"Health: {playerHealth.Health}";
        }
    }

    public void UpdateMoney() {
        if (null != bank) {
            moneyText.text = $"Money: {bank.CurrentBalance}";
        }
    }
}
