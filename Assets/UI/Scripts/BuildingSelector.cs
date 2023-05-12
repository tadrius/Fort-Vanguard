using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingSelector : MonoBehaviour
{

    [SerializeField] Building building;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] Image selectionIcon;

    BuildingPanel buildPanel;

    Builder builder;

    public void SetBuilding(Building building)
    {
        this.building = building;
    }

    void Awake() {
        buildPanel = FindObjectOfType<BuildingPanel>();
        builder = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Builder>();
    }

    private void Start()
    {
        if (building != null)
        {
            WriteBuildingInfo(); // write building info at start (text objects are null on awake)
        }
    }
    void WriteBuildingInfo() {
        nameText.text = building.BuildingName;
        costText.text = $"{building.Cost}";
    }

    public void SelectRefund()
    {
        selectionIcon.enabled = true;
        builder.SetToRefund();
        DeselectOtherSelectors();
    }


    public void SelectBuilding() {
        selectionIcon.enabled = true;
        builder.SetToConstruct(building);
        DeselectOtherSelectors();
    }

    void DeselectOtherSelectors()
    {
        // Deselect all other building selectors
        foreach (BuildingSelector selector in buildPanel.BuildSelectors)
        {
            if (this != selector)
            {
                selector.Deselect();
            }
        }
    }

    public void Deselect() {
        selectionIcon.enabled = false;
    }

}
