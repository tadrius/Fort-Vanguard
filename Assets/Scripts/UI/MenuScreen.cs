using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{

    [SerializeField] MenuScreen initialSubscreen;

    MenuScreen returnScreen;

    public void OpenScreen(MenuScreen returnScreen) {
        OpenScreen();
        if (null != returnScreen) {
            this.returnScreen = returnScreen;
            returnScreen.gameObject.SetActive(false);
        }
    }

    public void OpenScreen() {
        PauseGame();
        gameObject.SetActive(true);
        if (null != initialSubscreen) { // if an initial subscreen is set, close all subscreens and open it
            foreach (MenuScreen subscreen in GetComponentsInChildren<MenuScreen>()) {
                subscreen.gameObject.SetActive(false);
            }
            initialSubscreen.gameObject.SetActive(true);
        }
        gameObject.SetActive(true); // set this active last so it does not disable itself
    }

    public void CloseScreen() {
        this.gameObject.SetActive(false);
        if (null != returnScreen) {
            returnScreen.OpenScreen();
        } else {
            UnpauseGame(); // only unpause game if there is not another screen to return to
        }
    }

    public void StartGame() {
        CloseScreen();
    }

    public void RestartGame() {
        Player player = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Player>();
        player.ExecuteGameOverSequence();
    }

    public void Exit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void PauseGame() {
        Player player = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Player>();
        player.PauseGame();
    }

    public void UnpauseGame() {
        Player player = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Player>();
        player.UnpauseGame();
    }
}
