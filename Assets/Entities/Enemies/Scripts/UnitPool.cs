using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPool : ObjectPool
{

    [SerializeField] Tier unitTier = Tier.Standard;
    [SerializeField] Weight unitWeight = Weight.Medium;
    [Tooltip("The default faction for this pool.")]
    [SerializeField] Faction faction;

    enum Tier { Standard, Veteran, Champion }
    enum Weight { Light, Medium, Heavy };

    private void Awake()
    {
        FactionSettings factionSettings = FindObjectOfType<FactionSettings>();
        if (factionSettings != null)
        {
            faction = factionSettings.EnemyFaction;
        } 

        if (Tier.Standard == unitTier)
        {
            if (Weight.Light == unitWeight)
            {
                objectPrefab = faction.LightStandard.gameObject;
            } else if (Weight.Medium == unitWeight)
            {
                objectPrefab = faction.MediumStandard.gameObject;
            } else if (Weight.Heavy == unitWeight)
            {
                objectPrefab = faction.HeavyStandard.gameObject;
            }
        } else if (Tier.Veteran == unitTier)
        {
            if (Weight.Light == unitWeight)
            {
                objectPrefab = faction.LightVeteran.gameObject;
            }
            else if (Weight.Medium == unitWeight)
            {
                objectPrefab = faction.MediumVeteran.gameObject;
            }
            else if (Weight.Heavy == unitWeight)
            {
                objectPrefab = faction.HeavyVeteran.gameObject;
            }
        } else if (Tier.Champion == unitTier)
        {
            if (Weight.Light == unitWeight)
            {
                objectPrefab = faction.LightChampion.gameObject;
            }
            else if (Weight.Medium == unitWeight)
            {
                objectPrefab = faction.MediumChampion.gameObject;
            }
            else if (Weight.Heavy == unitWeight)
            {
                objectPrefab = faction.HeavyChampion.gameObject;
            }
        }
        PopulatePool();
    }

}
