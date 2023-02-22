using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{

    readonly public static string mainMenuTag = "MainMenu";

    bool isFirstLoad = true;

    public void Startup() {
        MenuScreen mainMenu = GameObject.FindGameObjectWithTag(mainMenuTag).GetComponent<MenuScreen>();
        if (isFirstLoad) { // only open the main menu the first time the scene loads
            Debug.Log("First-time load, opening main menu...");
            isFirstLoad = false;
            mainMenu.OpenScreen();
        } else { // otherwise close it
            mainMenu.CloseScreen();
        }
    }
}
