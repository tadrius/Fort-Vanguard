using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{

    [SerializeField] float loadDelay = 1f;
    [SerializeField] GameOverScreen gameOverScreen;
    [SerializeField] LoadingScreen loadingScreen;

    Scoreboard scoreboard;

    void Awake() {
        UnpauseGame();
        scoreboard = FindObjectOfType<Scoreboard>();
    }

    public void StartGame() {
        LoadNextScene();
    }

    public void RestartGame() {
        scoreboard.UpdateScoreboard();
        ReloadScene();
    }

    public void WinGame() {
        scoreboard.UpdateScoreboard();
        gameOverScreen.OpenWinScreen();
    }

    public void LoseGame() {
        scoreboard.UpdateScoreboard();
        gameOverScreen.OpenLoseScreen();
    }

    public void LoadMainMenu()
    {
        scoreboard.UpdateScoreboard();
        LoadScene(0, loadDelay);
    }

    public void PauseGame() {
        Time.timeScale = 0f;
    }

    public void UnpauseGame() {
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
