using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    [SerializeField] bool refunding = false;
    [SerializeField] Building building;

    public bool Refunding { get { return refunding; } }
    public Building Building { get { return building; } }

    public void SetRefunding(bool refunding)
    {
        this.refunding = refunding;
    }

    public void SetBuildingPrefab(Building building) {
        this.building = building;
    }

    public GameObject CreateBuilding(Tile tile)
    {
        RuntimeSpawns runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag)
            .GetComponent<RuntimeSpawns>();
        Bank bank = FindObjectOfType<Bank>();

        if (CheckSiteCompatibility(tile) && WithdrawCost(bank))
        {
            GameObject newBuilding = runtimeSpawns.SpawnObject(building.gameObject, tile.transform.position);
            newBuilding.GetComponent<Building>().SetIsElevated(tile.IsPlatform);
            return newBuilding;
        }
        return null;
    }

    public bool CheckSiteCompatibility(Tile tile)
    {
        if (tile.IsBuildSite && !tile.IsOccupied)
        {
            if (!tile.IsPlatform || (tile.IsPlatform && building.IsPlatformBuildable))
            {
                return true;
            }
        }
        return false;
    }

    bool WithdrawCost(Bank bank)
    {
        if (null != bank && bank.Withdraw(building.Cost))
        {
            return true;
        }
        return false;
    }

}
