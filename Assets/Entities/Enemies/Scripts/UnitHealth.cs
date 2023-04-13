using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Unit))]
public class UnitHealth : MonoBehaviour
{

    [SerializeField] int maxHitPoints = 10;
    [SerializeField] ParticleSystem damageParticles;

    int currentHitPoints = 0;
    Unit unit;

    void Start() {
        unit = GetComponent<Unit>();
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
        unit.SpawnDeathFX();
        unit.PlayDeathAnimation();
        unit.DepositReward();
        gameObject.SetActive(false);
    }

    void PlayDamageFX() {
        if (null != damageParticles) {
            damageParticles.Play();
        }
    }
}
