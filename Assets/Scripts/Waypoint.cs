using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] GameObject validSiteDisplay;
    [SerializeField] GameObject invalidSiteDisplay;

    [SerializeField] bool isValidSite = true;
    [SerializeField] bool isPlatformSite = false;

    public bool IsValidSite { get { return isValidSite; } } // property to expose isValidSite
    
    Builder builder;

    void Start() {
        builder = Builder.GetPlayerBuilder();
        DisableBuildSiteDisplays();
    }

    void OnMouseOver() {
        if (isValidSite && builder.Building.CheckPlatformCompatibility(isPlatformSite)) {
            validSiteDisplay.SetActive(true);
        } else {
            invalidSiteDisplay.SetActive(true);
        }
    }

    void OnMouseExit() {
        DisableBuildSiteDisplays();
    }

    void OnMouseDown() {
        Building buildling = builder.Building;

        if (isValidSite) {
            if (buildling.CreateBuilding(buildling, transform.position, isPlatformSite)) {
                isValidSite = false;
                DisableBuildSiteDisplays();
            }
        }
    }

    void DisableBuildSiteDisplays() {
        validSiteDisplay.SetActive(false);
        invalidSiteDisplay.SetActive(false);        
    }
}
