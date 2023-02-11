using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyHealth))]
[RequireComponent(typeof(EnemyMover))]
public class Enemy : MonoBehaviour
{
    [SerializeField] int pointReward = 1;
    [SerializeField] int goldReward = 7;
    [SerializeField] int healthPenalty = 20;
    [SerializeField] GameObject penaltyFX;
    [SerializeField] GameObject deathFX;

    ScoreKeeper scoreKeeper;
    Bank bank;
    PlayerHealth playerHealth;
    RuntimeSpawns runtimeSpawns;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag(Player.playerTag);
        runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag)
            .GetComponent<RuntimeSpawns>();
        scoreKeeper = player.GetComponent<ScoreKeeper>(); 
        bank = player.GetComponent<Bank>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public void DepositReward() {
        if (null != bank) {
            bank.Deposit(goldReward);
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

    public void SpawnDeathFX() {
        runtimeSpawns.SpawnObject(deathFX, transform.position);
    }

    public void SpawnPenaltyFX() {
        runtimeSpawns.SpawnObject(penaltyFX, transform.position);
    }
}
