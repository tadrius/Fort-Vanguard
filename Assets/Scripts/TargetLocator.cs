using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{

    [SerializeField] Transform weapon;
    [Tooltip("The maximum range at which an enemy will be targeted.")]
    [SerializeField] float range = 25f;
    ParticleSystem[] projectileParticleSystems;
    Transform target;

    // TODO - use enemies field
    // to track potential targets
    // Enemy[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        projectileParticleSystems = weapon.GetComponentsInChildren<ParticleSystem>();
        // TODO - use enemies field instead of repeated calls in FindClosestTarget
        // enemies = FindObjectsOfType<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetIsValid()) {
            ShootTarget(); 
        } else {
            DropTarget();
            FindClosestTarget();
        }
    }

    void FindClosestTarget() {
        Transform closestTarget = null;
        float maxDistance = range;
        // TODO - use enemies field instead of repeated calls in FindClosestTarget
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy en in enemies) {
            float enemyDistance = Vector3.Distance(transform.position, en.transform.position);
            if (en.gameObject.activeSelf && enemyDistance < maxDistance) {
                closestTarget = en.transform;
                maxDistance = enemyDistance;
            }
        }
        target = closestTarget;
    }

    bool TargetIsValid() {
        if (null != target && target.gameObject.activeSelf) {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            if (targetDistance <= range) {
                return true;
            }
        }
        return false;
    }

    void ShootTarget() {
        AimWeapon();
        setProjectileEmissionsEnabled(true);
    }

    void AimWeapon() {
        weapon.LookAt(target);
    }

    void DropTarget() {
        target = null;      
        setProjectileEmissionsEnabled(false);
    }

    void setProjectileEmissionsEnabled(bool enabled) {
        foreach (ParticleSystem projectileParticles in projectileParticleSystems) {
            var em = projectileParticles.emission;
            em.enabled = enabled;
        }
    }
}
