using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MenuScreen
{

    [SerializeField] string winTextString = "Victory!";
    [SerializeField] string loseTextString = "Defeat...";
    [SerializeField] TMP_Text titleText;

    public void OpenWinScreen() {
        titleText.text = winTextString;
        if (!gameObject.activeSelf) {
            Open();
        }
    }

    public void OpenLoseScreen() {
        titleText.text = loseTextString;
        if (!gameObject.activeSelf) {
            Open();
        }
    }

}
