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

    readonly public static string playerTag = "Player";

    Bank bank;
    PlayerHealth playerHealth;
    ScoreKeeper scoreKeeper;
    Builder builder;

    void Awake() {
        bank = GetComponent<Bank>();
        playerHealth = GetComponent<PlayerHealth>();
        scoreKeeper = GetComponent<ScoreKeeper>();
        builder = GetComponent<Builder>();
    }

    public void ExecuteGameOverSequence() {
        scoreKeeper.UpdateScoreboard();
        ReloadScene();
    }
    
    void ReloadScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadScene(currentScene);
    }

    void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.buildIndex);
    }

}
