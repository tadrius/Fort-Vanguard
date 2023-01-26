using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    [SerializeField] Building buildingPrefab;

    public Building Building { get { return buildingPrefab; } }

}
