using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{

    [SerializeField] MenuScreen initialSubscreen;

    MenuScreen returnScreen;

    public void Open(MenuScreen returnScreen) {
        Open();
        if (null != returnScreen) {
            this.returnScreen = returnScreen;
            returnScreen.gameObject.SetActive(false);
        }
    }

    public void Open() {
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

    public void Close() {
        this.gameObject.SetActive(false);
        if (null != returnScreen) {
            returnScreen.Open();
        } else {
            UnpauseGame(); // only unpause game if there is not another screen to return to
        }
    }

    public void StartGame() {
        Close();
    }

    public void RestartGame() {
        Player player = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Player>();
        player.RestartGame();
    }

    public void ExitGame() {
        Player player = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Player>();
        player.QuitGame();
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
