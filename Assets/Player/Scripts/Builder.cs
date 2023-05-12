using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ObjectChangeEventStream;

public class Builder : MonoBehaviour
{

    [SerializeField] Mode builderMode = Mode.Construct;
    [SerializeField] Building building;

    public Mode BuilderMode { get { return builderMode; } }
    public Building Building { get { return building; } }

    public enum Mode { Construct, Refund };

    public void SetBuilderMode(Mode mode)
    {
        this.builderMode = mode;
    }

    public void SetToRefund()
    {
        this.building = null;
        this.builderMode = Mode.Refund;
    }

    public void SetToConstruct(Building building) {
        this.building = building;
        this.builderMode = Mode.Construct;
    }

    public Building Build(Tile tile)
    {
        Building newBuilding = null;

        switch (builderMode)
        {
            case Mode.Construct:
                if (!tile.WillBlockPathfinding())
                {
                    newBuilding = CreateBuilding(tile);
                    if (null != newBuilding)
                    {
                        tile.BlockTile();
                    }
                }
                break;
            case Mode.Refund:
                if (null != tile.Building && tile.Building.RefundBuilding())
                {
                    tile.UnblockTile();
                }
                break;
        }
        return newBuilding;
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
