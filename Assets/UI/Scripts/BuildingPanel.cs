using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPanel : MonoBehaviour
{

    [SerializeField] BuildingSelector buildButton;
    [Tooltip("A positional offset applied to all buttons.")]
    [SerializeField] float buttonYOffset = 12f;
    [Tooltip("The amount of distance between each button.")]
    [SerializeField] float buttonSpacing = 24f;

    Faction playerFaction;
    List<BuildingSelector> buildSelectors = new List<BuildingSelector>();

    public List<BuildingSelector> BuildSelectors { get { return buildSelectors; } }

    private void Awake()
    {
        Player player = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Player>();
        playerFaction = player.PlayerFaction;
        List<Building> buildings = playerFaction.Constructables;
        BuildingSelector button;
        for (int i = 0; i < buildings.Count; i++) {
            Building building = buildings[i];
            if (null != building) {
                button = GameObject.Instantiate(buildButton, transform); // create the button
                button.SetBuilding(building); // assign the building

                Vector3 buttonPosition = Vector3.zero;
                buttonPosition.y = buttonYOffset + (i * buttonSpacing);
                button.GetComponent<RectTransform>().localPosition = buttonPosition; // position the button

                buildSelectors.Add(button); // add the button the list
            }
        }
        buildSelectors[buildSelectors.Count - 1].Select(); // select the last button
    }
}
