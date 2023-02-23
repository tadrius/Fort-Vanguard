using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    readonly public static string mainMenuTag = "MainMenu";

    bool isFirstLoad = true;

    public void Startup() {
        MenuScreen startMenu = GameObject.FindGameObjectWithTag(mainMenuTag).GetComponent<MenuScreen>();
        if (isFirstLoad) { // on the first load of the scene
            Debug.Log("First-time load, opening start menu...");
            isFirstLoad = false;
            startMenu.Open(); // open the start menu
        } else { // on subsequent loads
            startMenu.Close();
        }
    }
}
