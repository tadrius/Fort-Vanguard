using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealth : MonoBehaviour
{
    int maxHitPoints = 10;
    int currentHitPoints;

    Unit unit;
    Decomposer decomposer;

    void Start() {
        unit = transform.parent.GetComponent<Unit>();
        decomposer = transform.parent.GetComponent<Decomposer>();
    }

    public void SetHitPoints(int hitPoints) {
        this.maxHitPoints = hitPoints;
        this.currentHitPoints = hitPoints;
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
        decomposer.BeginDecomposing();

        // disable unit component and object with function components (including UnitHealth and UnitMover)
        unit.enabled = false;
        gameObject.SetActive(false);
    }

    void PlayDamageFX() {
        if (null != unit.DamageParticles) {
            unit.DamageParticles.Play();
        }
    }
}
