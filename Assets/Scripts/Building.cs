using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField] int cost = 50;

    Bank bank;
    GameObject runtimeSpawns;    
    readonly string runtimeSpawnsTag = "RuntimeSpawns";

    public bool CreateBuilding(Building prefab, Vector3 position) {
        runtimeSpawns = GameObject.FindGameObjectWithTag(runtimeSpawnsTag);
        bank = FindObjectOfType<Bank>();

        if (WithdrawCost()) {
            GameObject newBuilding = Instantiate(prefab.gameObject, position, Quaternion.identity);
            newBuilding.transform.parent = runtimeSpawns.transform;
            return true;
        }
        return false;
    }

    bool WithdrawCost() {
        if (null != bank && bank.Withdraw(cost)) {
            return true;
        }
        return false;
    }
}
