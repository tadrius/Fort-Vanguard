using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField] int cost = 50;
    [SerializeField] bool isPlatformBuildable = false;

    Bank bank;
    GameObject runtimeSpawns;
    readonly static string runtimeSpawnsTag = "RuntimeSpawns";

    public bool CreateBuilding(Building prefab, Vector3 position, bool isPlatformSite) {
        runtimeSpawns = GameObject.FindGameObjectWithTag(runtimeSpawnsTag);
        bank = FindObjectOfType<Bank>();
        
        if (CheckPlatformCompatibility(isPlatformSite) && WithdrawCost()) {
            GameObject newBuilding = Instantiate(prefab.gameObject, position, Quaternion.identity);
            newBuilding.transform.parent = runtimeSpawns.transform;
            return true;
        }
        return false;
    }

    public bool CheckPlatformCompatibility(bool isPlatformSite) {
        if ((isPlatformSite && isPlatformBuildable) || !isPlatformSite) {
            return true;
        }
        Debug.Log("Building cannot be constructed on platform");
        return false;
    }

    bool WithdrawCost() {
        if (null != bank && bank.Withdraw(cost)) {
            return true;
        }
        return false;
    }
}
