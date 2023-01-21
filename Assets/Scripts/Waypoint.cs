using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] GameObject building;
    [SerializeField] bool isValidSite = true;
    public bool IsValidSite { get { return isValidSite; }} // property to expose isValidSite

    static RuntimeSpawnsParent runtimeSpawnsParent;

    void Awake() {
        runtimeSpawnsParent = GameObject.FindObjectOfType<RuntimeSpawnsParent>();
    }

    void OnMouseDown() {
        if (isValidSite) {
            GameObject newBuilding = Instantiate(building, transform.position, Quaternion.identity);
            newBuilding.transform.parent = runtimeSpawnsParent.transform;
            isValidSite = false;
        }
    }
}
