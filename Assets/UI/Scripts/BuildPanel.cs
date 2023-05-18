using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPanel : MonoBehaviour
{

    [Tooltip("Generic selectors that will be added to the panel after the faction build selectors.")]
    [SerializeField] List<BuildSelector> standardSelectors = new List<BuildSelector>();
    [Tooltip("Prefab for selector buttons that will be generated for all of the player faction's buildings.")]
    [SerializeField] BuildSelector buildButton;
    [Tooltip("A positional offset applied to all buttons.")]
    [SerializeField] float buttonYOffset = 12f;
    [Tooltip("The amount of distance between each button.")]
    [SerializeField] float buttonSpacing = 24f;

    Faction playerFaction;
    List<BuildSelector> buildSelectors = new List<BuildSelector>();

    public List<BuildSelector> BuildSelectors { get { return buildSelectors; } }

    private void Awake()
    {
        AddFactionButtons();
        AddStandardButtons();
        buildSelectors[buildSelectors.Count - 1 - standardSelectors.Count].SelectBuilding(); // select the last faction button
    }

    void AddFactionButtons()
    {
        Player player = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Player>();
        playerFaction = player.PlayerFaction;
        List<Building> buildings = playerFaction.Constructables;
        for (int i = 0; i < buildings.Count; i++)
        {
            Building building = buildings[i];
            if (null != building)
            {
                BuildSelector button = GameObject.Instantiate(buildButton, transform); // create the button
                button.SetBuilding(building); // assign the building

                Vector3 buttonPosition = Vector3.zero;
                buttonPosition.y = buttonYOffset + (i * buttonSpacing);
                button.GetComponent<RectTransform>().localPosition = buttonPosition; // position the button

                buildSelectors.Add(button); // add the button the list
            }
        }
    }

    void AddStandardButtons()
    {
        for (int i = 0; i < standardSelectors.Count; i++)
        {
            BuildSelector button = GameObject.Instantiate(standardSelectors[i], transform); // create the button
            Vector3 buttonPosition = Vector3.zero;
            buttonPosition.y = buttonYOffset + (i * buttonSpacing) + (playerFaction.Constructables.Count * buttonSpacing);
            button.GetComponent<RectTransform>().localPosition = buttonPosition; // position the button

            buildSelectors.Add(button); // add the button the list
        }
    }
}
