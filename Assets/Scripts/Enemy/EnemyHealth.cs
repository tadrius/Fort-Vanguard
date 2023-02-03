using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{

    [SerializeField] int maxHitPoints = 10;
    [Tooltip("By how much the enemy's health will increase each time it is killed, " 
        + "resulting in stronger enemies over time.")]
    [SerializeField] int hitPointRamp = 1;
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] GameObject deathFX;

    int currentHitPoints = 0;
    Enemy enemy;
    GameObject runtimeSpawns;

    void Start() {
        runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag);
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
        SpawnDeathFX();
        enemy.DepositReward();
        gameObject.SetActive(false);
        maxHitPoints += hitPointRamp;     
    }

    void PlayDamageFX() {
        PlayParticles(damageParticles);
    }

    void PlayParticles(ParticleSystem particles) {
        if (null != particles) {
            particles.Play();
        }
    }

    void SpawnDeathFX() {
        GameObject newDeathFX = Instantiate(deathFX, transform.position, Quaternion.identity);
        newDeathFX.transform.parent = runtimeSpawns.transform;
    }
}
