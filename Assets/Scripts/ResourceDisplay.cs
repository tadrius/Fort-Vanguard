using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceDisplay : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] TMP_Text healthText;
    [SerializeField] TMP_Text moneyText;

    Bank bank;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        bank = player.GetComponent<Bank>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update() {
        UpdateHealth();
        UpdateMoney();        
    }

    public void UpdateHealth() {
        healthText.text = $"Health: {playerHealth.Health}";
    }

    public void UpdateMoney() {
        moneyText.text = $"Money: {bank.CurrentBalance}";
    }
}
