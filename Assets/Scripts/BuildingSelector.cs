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

    Builder builder;

    void Awake() {
        builder = Builder.GetPlayerBuilder();
        WriteBuildingInfo();
    }

    void WriteBuildingInfo() {
        nameText.text = building.name;
        costText.text = $"{building.Cost}";
    }

    public void OnButtonPress() {
        builder.SetBuildingPrefab(building);
    }

}
