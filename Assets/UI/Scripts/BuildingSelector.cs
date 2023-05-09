using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSelector : MonoBehaviour
{

    [Tooltip("Indicates if this selector is activated at the start. Only one selector should have this enabled.")]
    [SerializeField] bool isInitialSelection = false;
    [SerializeField] Building building;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] Image selectionIcon;

    BuildingSelector[] selectors;

    Builder builder;

    void Awake() {
        selectors = FindObjectsOfType<BuildingSelector>();
        builder = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Builder>();
        WriteBuildingInfo();
        if (isInitialSelection) {
            Select();
        }
    }

    void WriteBuildingInfo() {
        nameText.text = building.BuildingName;
        costText.text = $"{building.Cost}";
    }

    public void Select() {
        selectionIcon.enabled = true;
        builder.SetBuildingPrefab(building);

        // Deselect all other building selectors
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
