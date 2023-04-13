using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Tooltip("The maximum range at which an enemy will be targeted.")]
    [SerializeField] float range = 50f;
    [Tooltip("How much time must pass between each attack.")]
    [SerializeField] float attackDelay = 2f;
    [SerializeField] AudioSource attackAudio;

    ParticleSystem[] projectileParticleSystems;
    Transform target;
    Building building;

    float attackTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        projectileParticleSystems = GetComponentsInChildren<ParticleSystem>();
        building = GetComponentInParent<Building>();
        if (building.IsElevated) {
            range *= 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAttackTimer();
        if (TargetIsValid()) {
            AimWeapon(); 
            AttackTarget();
        } else {
            DropTarget();
            FindClosestTarget();
        }
    }

    void FindClosestTarget() {
        Transform closestTarget = null;
        float maxDistance = range;
        Unit[] enemies = FindObjectsOfType<Unit>();
        foreach (Unit en in enemies) {
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

    void UpdateAttackTimer() {
        if (attackTimer > 0) {
            attackTimer -= Time.deltaTime;
        }
    }

    void AttackTarget() {
        if (attackTimer <= 0) {
            EmitProjectileParticles();
            if (null != attackAudio) {
                attackAudio.Play();
            }
            attackTimer = attackDelay;
        }
    }

    void AimWeapon() {
        transform.LookAt(target);
    }

    void DropTarget() {
        target = null;      
    }

    void EmitProjectileParticles() {
        foreach (ParticleSystem projectileParticles in projectileParticleSystems) {
            projectileParticles.Emit(1);
        }
    }
}
