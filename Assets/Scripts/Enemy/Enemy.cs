using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int pointReward = 1;
    [SerializeField] int coinReward = 5;
    [SerializeField] int healthPenalty = 20;

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
    }

    public void DepositReward() {
        if (null != bank) {
            bank.Deposit(coinReward);
        }
        if (null != scoreKeeper) {
            scoreKeeper.AddToScore(pointReward);
        }
    }

    public void IncurPenalty() {
        if (null != playerHealth) {
            playerHealth.Damage(healthPenalty);
        }
    }
}
