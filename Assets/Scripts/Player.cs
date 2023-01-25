using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] int health = 100;
    
    public void Damage(int damageAmount) {
        health -= damageAmount;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        ReloadScene();
    }

    void ReloadScene() {
        Scene currentScene = SceneManager.GetActiveScene();
        LoadScene(currentScene);
    }

    void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.buildIndex);
    }
}
