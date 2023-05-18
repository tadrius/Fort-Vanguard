using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildSelector : MonoBehaviour
{

    [SerializeField] Building building;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] Image selectionIcon;

    BuildPanel buildPanel;

    Builder builder;

    public void SetBuilding(Building building)
    {
        this.building = building;
    }

    void Awake() {
        buildPanel = FindObjectOfType<BuildPanel>();
        builder = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Builder>();
    }

    private void Start()
    {
        WriteButtonInfo();
        if (building != null)
        {
            WriteBuildingInfo(); // write building info at start (text objects are null on awake)
        }
    }

    void WriteButtonInfo()
    {
        if ("Refund".Equals(nameText.text))
        {
            Builder builder = GameObject.FindObjectOfType<Builder>();
            costText.text = $"{100f * builder.RefundMultiplier}%"; // write the refund amount as a percentage
        }
        else if (building != null)
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
        foreach (BuildSelector selector in buildPanel.BuildSelectors)
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
