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
    [SerializeField] float loadDelay = 1f;
    [SerializeField] GameOverScreen gameOverScreen;
    [SerializeField] LoadingScreen loadingScreen;

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

    public void WinGame() {
        scoreKeeper.UpdateScoreboard();
        gameOverScreen.OpenWinScreen();
    }

    public void LoseGame() {
        scoreKeeper.UpdateScoreboard();
        gameOverScreen.OpenLoseScreen();
    }

    public void StartGame() {
        LoadNextScene();
    }

    public void RestartGame() {
        scoreKeeper.UpdateScoreboard();
        ReloadScene();
    }

    public void PauseGame() {
        Debug.Log("Game paused.");
        Time.timeScale = 0f;
    }

    public void UnpauseGame() {
        Debug.Log("Game unpaused.");
        Time.timeScale = 1f;
    }

    public void QuitGame() {
        StartCoroutine(QuitApplicationWithDelay(loadDelay));
    }

    IEnumerator QuitApplicationWithDelay(float loadDelay) {
        // delay
        for (float time = loadDelay; time >= 0; time -= Time.unscaledDeltaTime)
        {
            yield return null;
        }
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void LoadNextScene() {
        LoadScene(SceneManager.GetActiveScene().buildIndex + 1, loadDelay);
    }

    void ReloadScene() {
        LoadScene(SceneManager.GetActiveScene().buildIndex, loadDelay);
    }

    void LoadScene(int sceneBuildIndex, float loadDelay) {
        StartCoroutine(LoadSceneWithDelay(sceneBuildIndex, loadDelay));
    }

    IEnumerator LoadSceneWithDelay(int sceneBuildIndex, float loadDelay) {
        loadingScreen.gameObject.SetActive(true);
        // delay
        for (float time = loadDelay; time >= 0; time -= Time.unscaledDeltaTime)
        {
            yield return null;
        }
        // load next scene if scene count is greater than next scene index
        if (SceneManager.sceneCountInBuildSettings > sceneBuildIndex)
        {
            loadingScreen.LoadScene(sceneBuildIndex);
        }
        else
        {
            Debug.Log("Scene build index is greater than scene count. Loading the first scene.");
            loadingScreen.LoadScene(0);
        }
    }

}
