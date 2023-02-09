using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuToggler : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject menuPanel;
    [SerializeField] Image selectionIcon;

    readonly static string MenuText = "Menu";
    readonly static string ResumeText = "Resume";

    bool menuIsActive = false;

    void Awake() {
        menuPanel.SetActive(menuIsActive);
        selectionIcon.enabled = menuIsActive;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleMenu();
        }
    }

    public void ToggleMenu() {
        menuIsActive = !menuIsActive;

        menuPanel.SetActive(menuIsActive);
        selectionIcon.enabled = menuIsActive;

        if (menuIsActive) {
            text.text = ResumeText;
        } else {
            text.text = MenuText;
        }
    }

}
