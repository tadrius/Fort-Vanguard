using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    [SerializeField] Building buildingPrefab;

    readonly static string playerTag = "Player";

    public Building BuildingPrefab { get { return buildingPrefab; } }

    public void SetBuildingPrefab(Building building) {
        buildingPrefab = building;
    }

    public static Builder GetPlayerBuilder() {
        GameObject player = GameObject.FindGameObjectWithTag(playerTag);
        if (null != player) {
            return player.GetComponent<Builder>();
        }
        Debug.Log("Cannot find player or player's builder component.");
        return null;
    }

}
