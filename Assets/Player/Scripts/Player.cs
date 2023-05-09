using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Bank))]
[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(ScoreKeeper))]
[RequireComponent(typeof(Builder))]
public class Player : MonoBehaviour
{
    [SerializeField] Faction playerFaction;

    readonly public static string playerTag = "Player";

    Bank bank;
    PlayerHealth playerHealth;
    ScoreKeeper scoreKeeper;
    Builder builder;

    public Faction PlayerFaction { get { return playerFaction; } }
    public ScoreKeeper ScoreKeeper { get { return scoreKeeper; } }

    void Awake() {
        bank = GetComponent<Bank>();
        playerHealth = GetComponent<PlayerHealth>();
        scoreKeeper = GetComponent<ScoreKeeper>();
        builder = GetComponent<Builder>();
    }

}
