using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FactionSelector : MonoBehaviour
{

    TMP_Dropdown dropdown;
    FactionSettings settings;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        settings = FindObjectOfType<FactionSettings>();
        dropdown.ClearOptions();

        // add factions as option data
        List<TMP_Dropdown.OptionData> factionOptionDataList = new List<TMP_Dropdown.OptionData>();
        foreach (Faction faction in settings.Factions)
        {
            TMP_Dropdown.OptionData factionOptionData = new TMP_Dropdown.OptionData();
            factionOptionData.text = faction.FactionName;
            factionOptionDataList.Add(factionOptionData);
        }
        dropdown.AddOptions(factionOptionDataList);
        dropdown.value = 0;
    }

    private void Start()
    {
        SelectFaction();
    }

    public void SelectFaction()
    {
        TMP_Dropdown.OptionData factionOptionData = dropdown.options[dropdown.value];
        settings.SetPlayerFaction(factionOptionData.text);
    }

}
