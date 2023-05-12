using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{

    [SerializeField] GameObject validSiteDisplay;
    [SerializeField] GameObject invalidSiteDisplay;
    [SerializeField] GameObject pathDisplay;

    [SerializeField] bool isOccupied = false;
    [SerializeField] bool isBuildSite = true;
    [SerializeField] bool isPlatform = false;

    public bool IsOccupied { get { return isOccupied; } }
    public bool IsBuildSite { get { return isBuildSite; } }
    public bool IsPlatform { get { return isPlatform; } } 

    GridManager gridManager;
    Pathfinder pathfinder;
    Builder builder;
    GameObject buildingObject;

    Vector2Int coordinates = new Vector2Int();

    void Awake() {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
        builder = GameObject.FindGameObjectWithTag(Player.playerTag).GetComponent<Builder>();
    }

    void Start() {
        if (null != gridManager) {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        if (isOccupied) {
            gridManager.BlockNode(coordinates);
        }
        DisableBuildSiteDisplays();
    }

    void Update() {
        DisplayPathFX();
    }

    void OnMouseOver() {
        if (EventSystem.current.IsPointerOverGameObject()) { // return if a UI element is being pointed at
            OnMouseExit();
            return;
        }

        if (builder.CheckSiteCompatibility(this) & !WillBlockPathfinding()) {
            validSiteDisplay.SetActive(true);
        } else {
            invalidSiteDisplay.SetActive(true);
        }
    }

    void OnMouseExit() {
        DisableBuildSiteDisplays();
    }

    void OnMouseDown() {
        if (EventSystem.current.IsPointerOverGameObject()) { // return if a UI element is being pointed at
            return;
        }

        Building building;
        if (null != buildingObject) {
            // if a building exists on this tile, attempt to destroy it
            building = buildingObject.GetComponent<Building>();
            if (building.RefundBuilding()) {
                UnblockTile();
            }
        } else { 
            // otherwise, create a new building
            if (!WillBlockPathfinding()) {
                buildingObject = builder.CreateBuilding(this);
                if (null != buildingObject) {
                    BlockTile();
                }
            }
        }
        DisableBuildSiteDisplays();
    }

    void DisplayPathFX() {
        pathDisplay.SetActive(true);
        if (null != gridManager) {
            Node node = gridManager.GetNode(coordinates);
            if (null != node && node.isPath) {
                pathDisplay.SetActive(true);
                return;
            }
        }
        pathDisplay.SetActive(false);
    }

    // checks if blocking this tile's associated node will interfere with pathfinding
    bool WillBlockPathfinding() {
        if (null != gridManager.GetNode(coordinates) 
            && gridManager.GetNode(coordinates).isTraversable ) {
            return pathfinder.WillBlockPath(coordinates);
        };
        return false; // if tile is not involved with pathfinding then building will not block
    }

    void BlockTile() {
        isOccupied = true;
        gridManager.BlockNode(coordinates);
        pathfinder.NotifyReceivers();
    }

    void UnblockTile() {
        isOccupied = false;
        gridManager.UnblockNode(coordinates);
        pathfinder.NotifyReceivers();  
    }

    void DisableBuildSiteDisplays() {
        validSiteDisplay.SetActive(false);
        invalidSiteDisplay.SetActive(false);        
    }
}
