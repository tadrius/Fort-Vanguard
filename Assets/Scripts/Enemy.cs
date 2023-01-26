using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int reward = 15;
    [SerializeField] int penalty = 15;

    Bank bank;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void DepositReward() {
        if (null != bank) {
            bank.Deposit(reward);
        }
    }

    public void IncurPenalty() {
        // if (null != bank) {
        //     bank.Withdraw(penalty);
        // }
        if (null != playerHealth) {
            playerHealth.Damage(penalty);
        }
    }
}
