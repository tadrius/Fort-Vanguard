using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{

    [SerializeField] int startingBalance = 100;
    // TODO - remove SerializeField
    [SerializeField] int currentBalance;

    public int CurrentBalance { get { return currentBalance; } }

    void Awake() {
        currentBalance = startingBalance;
    }

    public bool Deposit(int amount) {
        if (amount < 0) {
            Debug.Log("Cannot deposit a negative amount. Returning...");
            return false;
        }
        currentBalance += amount;
        return true;
    }

    public bool Withdraw(int amount) {
        if (amount < 0) {
            Debug.Log("Cannot withdraw a negative amount. Returning...");
            return false;
        } else if (currentBalance < amount) {
            Debug.Log("Cannot withdraw. Current balance is less than withdraw amount.");
            return false;
        } else {
            currentBalance -= amount;
            Debug.Log($"Withdrew {amount}.");
            return true;
        }
    }

}
