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

    TMP_Text text;

    void Awake() {
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
        screen.OpenScreen(returnScreen);
    }

    void CloseScreen() {
        screen.CloseScreen();
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

}
