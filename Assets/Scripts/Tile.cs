using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] GameObject validSiteDisplay;
    [SerializeField] GameObject invalidSiteDisplay;

    [SerializeField] bool isValidSite = true;
    [SerializeField] bool isPlatformSite = false;

    public bool IsValidSite { get { return isValidSite; } }
    public bool IsPlatformSite { get { return isPlatformSite; } } 

    GridManager gridManager;
    Builder builder;
    GameObject buildingObject;

    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        builder = Builder.GetPlayerBuilder();
    }

    void Start() {
        if (null != gridManager) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        if (!isValidSite) {
            gridManager.BlockNode(coordinates);
        }
        DisableBuildSiteDisplays();
    }

    void OnMouseOver() {
        if (builder.BuildingPrefab.CheckSiteCompatibility(this)) {
            validSiteDisplay.SetActive(true);
        } else {
            invalidSiteDisplay.SetActive(true);
        }
    }

    void OnMouseExit() {
        DisableBuildSiteDisplays();
    }

    void OnMouseDown() {
        Building building;

        if (null != buildingObject) {
            // if a building exists on this tile, attempt to destroy it
            building = buildingObject.GetComponent<Building>();
            if (building.DestroyBuilding()) {
                isValidSite = true;
            }
        } else { 
            // otherwise, create a new building
            building = builder.BuildingPrefab;
            buildingObject = building.CreateBuilding(building, transform.position, this);
            if (null != buildingObject) {
                isValidSite = false;
            }
        }
        DisableBuildSiteDisplays();
    }

    void DisableBuildSiteDisplays() {
        validSiteDisplay.SetActive(false);
        invalidSiteDisplay.SetActive(false);        
    }
}
