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

    public GameObject CreateBuilding(Building prefab, Vector3 position, Waypoint wp) {
        GameObject runtimeSpawns = GameObject.FindGameObjectWithTag(
            RuntimeSpawns.runtimeSpawnsTag);
        Bank bank = FindObjectOfType<Bank>();
        
        if (CheckSiteCompatibility(wp) && WithdrawCost(bank)) {
            GameObject newBuilding = Instantiate(prefab.gameObject, position, Quaternion.identity);
            newBuilding.GetComponent<Building>().isElevated = wp.IsPlatformSite;
            newBuilding.transform.parent = runtimeSpawns.transform;
            return newBuilding;
        }
        return null;
    }

    public bool DestroyBuilding() {
        Waypoint[] waypoints = GetComponentsInChildren<Waypoint>();
        // if this building has any child waypoints and any of these waypoints are in use
        // then this building is being used as a platform and cannot be destroyed
        foreach (Waypoint wp in waypoints) {
            if (!wp.IsValidSite) {
                Debug.Log("Cannot destroy a building being used as a platform.");
                return false;
            }        
        }
        // otherwise destroy the building
        DepositRefund(FindObjectOfType<Bank>());
        Destroy(gameObject);
        return true;
    }

    public bool CheckSiteCompatibility(Waypoint wp) {
        if (wp.IsValidSite) {
            if (!wp.IsPlatformSite || (wp.IsPlatformSite && isPlatformBuildable)) {
                return true;
            }
            Debug.Log("Building cannot be constructed on platform.");
        }
        Debug.Log("Invalid construction site.");
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
}
