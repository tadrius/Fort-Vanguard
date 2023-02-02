using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSelector : MonoBehaviour
{
    [SerializeField] bool isInitialSelection = false;
    [SerializeField] Building building;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] Image selectionIcon;

    Builder builder;

    void Awake() {
        builder = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Builder>();
        WriteBuildingInfo();
        if (isInitialSelection) {
            Select();
        }
    }

    void WriteBuildingInfo() {
        nameText.text = building.name;
        costText.text = $"{building.Cost}";
    }

    public void Select() {
        selectionIcon.enabled = true;
        builder.SetBuildingPrefab(building);

        // Deselect all other building selectors
        BuildingSelector[] selectors = FindObjectsOfType<BuildingSelector>();
        foreach (BuildingSelector selector in selectors) {
            if (this != selector) {
                selector.Deselect();
            }           
        }    
    }

    public void Deselect() {
        selectionIcon.enabled = false;
    }

}
