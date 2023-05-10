using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : MonoBehaviour
{
    
    [SerializeField] FactionName factionName = FactionName.Duke;

    [Header("Player Entities")]
    [SerializeField] Building playerHeadquarters;
    [Tooltip("All constructable buildings. Order of list determines order of buttons in the building panel.")]
    [SerializeField] List<Building> constructables;

    [Header("Enemy Entities")]
    [SerializeField] Building enemyHeadquarters;
    [SerializeField] Unit heavyStandard;
    [SerializeField] Unit mediumStandard;
    [SerializeField] Unit lightStandard;
    [SerializeField] Unit heavyVeteran;
    [SerializeField] Unit mediumVeteran;
    [SerializeField] Unit lightVeteran;
    [SerializeField] Unit heavyChampion;
    [SerializeField] Unit mediumChampion;
    [SerializeField] Unit lightChampion;

    public enum FactionName { Duke, Sheikh, Jarl };
    public List<Building> Constructables { get { return constructables; } }

}
