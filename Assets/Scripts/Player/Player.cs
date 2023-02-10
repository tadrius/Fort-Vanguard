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

    [SerializeField] MenuScreen mainMenu;

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

    void Start() {
        mainMenu.OpenScreen();
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
        UnpauseGame();
    }

    public void PauseGame() {
        Time.timeScale = 0f;
    }

    public void UnpauseGame() {
        Time.timeScale = 1f;
    }

}
