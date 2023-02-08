using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    Player player;

    void Awake() {
        GameObject playerObject = GameObject.FindGameObjectWithTag(Player.playerTag);
        if (null != playerObject) {
            player = playerObject.GetComponent<Player>();
        }
    }

    void OnEnable() {
        player.PauseGame();
    }

    void OnDisable() {
        player.UnpauseGame();
    }

    public void Restart() {
        player.ExecuteGameOverSequence();
    }

    public void Exit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
