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
    
    bool hotkeyEnabled = false;
    TMP_Text text;

    void Awake() {
        hotkeyEnabled = false;
        text = GetComponentInChildren<TMP_Text>();
    }

    void OnEnable() {
        UpdateTogglerVisual();
        screen.gameObject.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (hotkeyEnabled) {
                GetComponent<Button>().onClick.Invoke();
            }
        }
    }

    public void ToggleScreen() {
        if (screen.gameObject.activeSelf) {
            CloseScreen();
        } else {
            OpenScreen();
        }
        UpdateTogglerVisual();
    }

    void OpenScreen() {
        screen.Open(returnScreen);
    }

    void CloseScreen() {
        screen.Close();
    }

    void UpdateTogglerVisual() {
        if (null != selectionIcon) {
            selectionIcon.enabled = screen.gameObject.activeSelf;
        }
        if (screen.gameObject.activeSelf) {
            text.text = screenIsActiveText;
        } else {
            text.text = screenIsInactiveText;
        }
    }

    public void EnableHotkey() {
        hotkeyEnabled = true;
    }

}
