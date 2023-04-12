using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHitPoints = 10;
    [Tooltip("By how much the enemy's health will increase each time it is killed, " 
        + "resulting in stronger enemies over time.")]
    [SerializeField] int hitPointRamp = 1; // TODO - can remove after waves are implemented
    [SerializeField] ParticleSystem damageParticles;

    int currentHitPoints = 0;
    Enemy enemy;

    void Start() {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    void OnParticleCollision(GameObject other) {
        currentHitPoints--;
        PlayDamageFX();
        if (0 >= currentHitPoints) {
            Kill();
        }
    }

    void Kill() {
        enemy.SpawnDeathFX();
        enemy.DepositReward();
        gameObject.SetActive(false);
        maxHitPoints += hitPointRamp;     
    }

    void PlayDamageFX() {
        if (null != damageParticles) {
            damageParticles.Play();
        }
    }
}
