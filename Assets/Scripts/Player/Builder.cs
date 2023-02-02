using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    [SerializeField] Building building;

    public Building Building { get { return building; } }

    public void SetBuildingPrefab(Building building) {
        this.building = building;
    }

}
