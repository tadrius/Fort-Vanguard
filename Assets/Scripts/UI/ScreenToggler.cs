using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScreenToggler : MonoBehaviour
{
    [SerializeField] MenuScreen screen;
    [SerializeField] MenuScreen returnScreen;
    [SerializeField] string screenIsActiveText = "Return";
    [SerializeField] string screenIsInactiveText = "Menu";
    [SerializeField] Image selectionIcon;
    [SerializeField] bool hotkeyEnabled;
    [SerializeField] bool screenIsActive = false;

    TMP_Text text;

    void Awake() {
        text = GetComponentInChildren<TMP_Text>();
        CloseScreen();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (hotkeyEnabled) {
            GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    public void ToggleScreen() {
        if (screenIsActive) {
            CloseScreen();
        } else {
            OpenScreen();
        }
    }

    public void OpenScreen() {
        screenIsActive = true;
        UpdateTogglerVisual();
        screen.OpenScreen(returnScreen);
    }

    public void CloseScreen() {
        screenIsActive = false;
        UpdateTogglerVisual();
        screen.CloseScreen();
    }

    void UpdateTogglerVisual() {
        if (null != selectionIcon) {
            selectionIcon.enabled = screenIsActive;
        }
        if (screenIsActive) {
            text.text = screenIsActiveText;
        } else {
            text.text = screenIsInactiveText;
        }
    }

}
