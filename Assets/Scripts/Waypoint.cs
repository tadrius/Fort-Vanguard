using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] Building buildingPrefab;
    [SerializeField] bool isValidSite = true;
    public bool IsValidSite { get { return isValidSite; } } // property to expose isValidSite

    void OnMouseDown() {
        if (isValidSite) {
            if (buildingPrefab.CreateBuilding(buildingPrefab, transform.position)) {
                isValidSite = false;
            }
        }
    }
}
