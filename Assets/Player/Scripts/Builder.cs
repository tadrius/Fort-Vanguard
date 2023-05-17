using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;

public class Builder : MonoBehaviour
{

    [SerializeField] Mode buildMode = Mode.Construct;
    [SerializeField] Building building;
    [SerializeField] float refundMultiplier = 0.5f;

    List<Building> existingBuildings = new List<Building>();
    public Mode BuildMode { get { return buildMode; } }
    public Building Building { get { return building; } }
    public float RefundMultiplier { get { return refundMultiplier; } }
    public List<Building> ExistingBuildings { get { return existingBuildings; } }

    public enum Mode { Construct, Refund };

    public void SetBuilderMode(Mode mode)
    {
        this.buildMode = mode;
    }

    public void SetToRefund()
    {
        this.building = null;
        this.buildMode = Mode.Refund;
    }

    public void SetToConstruct(Building building) {
        this.building = building;
        this.buildMode = Mode.Construct;
    }

    public void Build(Tile tile)
    {
        switch (buildMode)
        {
            case Mode.Construct:
                if (!tile.WillBlockPathfinding())
                {
                    Building newBuilding = CreateBuilding(tile);
                    existingBuildings.Add(newBuilding);
                    if (null != newBuilding)
                    {
                        tile.SetBuilding(newBuilding);
                        tile.BlockTile();
                    }
                }
                break;
            case Mode.Refund:
                if (null != tile.Building)
                {
                    existingBuildings.Remove(tile.Building);
                    tile.Building.RefundBuilding(refundMultiplier);
                    tile.SetBuilding(null);
                    tile.UnblockTile();
                }
                break;
        }
    }

    public Building CreateBuilding(Tile tile)
    {
        RuntimeSpawns runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag)
            .GetComponent<RuntimeSpawns>();
        Bank bank = FindObjectOfType<Bank>();

        if (CheckSiteCompatibility(tile) && WithdrawCost(bank))
        {
            GameObject newBuildingObjecct = runtimeSpawns.SpawnObject(building.gameObject, tile.transform.position);
            Building newBuilding = newBuildingObjecct.GetComponent<Building>();
            newBuilding.SetIsElevated(tile.IsPlatform);
            return newBuilding;
        }
        return null;
    }

    public bool CheckSiteCompatibility(Tile tile)
    {
        switch (buildMode)
        {
            case Mode.Construct:
                if (tile.IsBuildSite && !tile.IsOccupied)
                {
                    if (!tile.IsPlatform || (tile.IsPlatform && building.IsPlatformBuildable))
                    {
                        return true;
                    }
                }
                break;
            case Mode.Refund:
                if (null != tile.Building)
                {
                    return true;
                }
                break;
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
