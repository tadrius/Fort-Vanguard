using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField] int cost = 50;
    [SerializeField] bool isPlatformBuildable = false;

    bool isElevated = false;

    public int Cost { get { return cost; }}
    public bool IsElevated { get { return isElevated; }}

    public GameObject CreateBuilding(Tile tile) {
        GameObject runtimeSpawns = GameObject.FindGameObjectWithTag(
            RuntimeSpawns.runtimeSpawnsTag);
        Bank bank = FindObjectOfType<Bank>();
        
        if (CheckSiteCompatibility(tile) && WithdrawCost(bank)) {
            GameObject newBuilding = Instantiate(
                gameObject, tile.transform.position, Quaternion.identity);
            newBuilding.GetComponent<Building>().SetIsElevated(tile.IsPlatformSite);
            newBuilding.transform.parent = runtimeSpawns.transform;
            return newBuilding;
        }
        return null;
    }

    public bool RefundBuilding() {
        if (DestroyBuilding()) {
            return DepositRefund(FindObjectOfType<Bank>());
        }
        return false;
    }

    public bool DestroyBuilding() {
        Tile[] tiles = GetComponentsInChildren<Tile>();
        // if this building has any child tiles and any of these tiles are in use
        // then this building is being used as a platform and cannot be destroyed
        foreach (Tile tile in tiles) {
            if (!tile.IsValidSite) {
                Debug.Log("Cannot destroy a building being used as a platform.");
                return false;
            }        
        }
        // otherwise destroy the building
        Destroy(gameObject);
        return true;
    }

    public bool CheckSiteCompatibility(Tile tile) {
        if (tile.IsValidSite) {
            if (!tile.IsPlatformSite || (tile.IsPlatformSite && isPlatformBuildable)) {
                return true;
            }
        }
        return false;
    }

    bool WithdrawCost(Bank bank) {
        if (null != bank && bank.Withdraw(cost)) {
            return true;
        }
        return false;
    }

    bool DepositRefund(Bank bank) {
        if (null != bank && bank.Deposit(cost/2)) {
            return true;
        }
        return false;
    }

    public void SetIsElevated(bool isElevated) {
        this.isElevated = isElevated;
    }
}
