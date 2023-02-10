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
        UpdateHighScore();
    }

    void Update() {
        UpdateScore();
        UpdateHealth();
        UpdateGold();        
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
}
