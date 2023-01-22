using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform weapon;
    ParticleSystem[] weaponParticleSystems;
    Transform target;

    // Start is called before the first frame update
    void Start()
    {
        weaponParticleSystems = weapon.GetComponentsInChildren<ParticleSystem>();
        LocateEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        AimWeapon();
        if (null == target) {
            setWeaponParticleEmissions(false);
            LocateEnemy();
        }
    }

    void LocateEnemy() {
        EnemyMover enemy = FindObjectOfType<EnemyMover>();
        if (null != enemy) {
            target = enemy.transform;
            setWeaponParticleEmissions(true);
        }
    }

    void setWeaponParticleEmissions(bool enabled) {
        foreach (ParticleSystem weaponParticles in weaponParticleSystems) {
            var em = weaponParticles.emission;
            em.enabled = enabled;
        }       
    }

    void AimWeapon() {
        weapon.LookAt(target);
    }
}
