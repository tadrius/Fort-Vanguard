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

    public void Deposit(int amount) {
        if (amount < 0) {
            Debug.Log("Cannot deposit a negative amount. Returning...");
            return;
        }
        currentBalance += amount;
    }

    public void Withdraw(int amount) {
        if (amount < 0) {
            Debug.Log("Cannot withdraw a negative amount. Returning...");
            return;
        }
        if (currentBalance >= amount) {
            currentBalance -= amount;
        } else {
            Debug.Log("Cannot withdraw. Current balance is less than withdraw amount.");
        }
    }

}
