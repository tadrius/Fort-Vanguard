using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health = 100;
    
    Player player;

    void Awake() {
        player = GetComponent<Player>();
    }

    public int Health { get { return health; }}

    public void Damage(int damageAmount) {
        health -= damageAmount;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        player.ExecuteGameOverSequence();
    }
}
