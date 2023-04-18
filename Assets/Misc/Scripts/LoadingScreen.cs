using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    [SerializeField] Slider loadingBar;

    public void LoadScene(int sceneBuildIndex) {
        StartCoroutine(LoadSceneAsync(sceneBuildIndex));
    }

    IEnumerator LoadSceneAsync(int sceneBuildIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildIndex);
        gameObject.SetActive(true);
        while (!operation.isDone) {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            loadingBar.value = progressValue;
            yield return null;
        }
    }

}
