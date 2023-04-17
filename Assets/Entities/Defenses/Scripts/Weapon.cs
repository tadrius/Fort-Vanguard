using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    [Tooltip("The maximum range at which an enemy will be targeted.")]
    [SerializeField] float range = 50f;
    [Tooltip("How much time must pass between each attack animation. Set to 0 to use the default animation length.")]
    [SerializeField] float reloadSpeed = 1f;
    [Tooltip("How quickly the attack animation plays. Set to 0 to use the default animation length.")]
    [SerializeField] float attackSpeed = 0f;
    [SerializeField] AudioSource attackAudio;

    ParticleSystem[] projectileParticleSystems;
    Transform target;
    Building building;
    CharacterAnimator animator;

    int currentAction = 0; // 0 = Idle, 1 = Walk, 2 = Aim, 3 = Attack, 4 = Reload, 5 = Death, 6 = Special

    float reloadTimer = 0f;

    void Awake() {
        animator = GetComponentInChildren<CharacterAnimator>();
    }

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
        Reload();
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

    void Reload() {
        if ((0 == currentAction || 4 == currentAction) && 0f < reloadTimer) { // if idle or reloading
            currentAction = 4; // set to reloading
            animator.UseReloadAnimations();
            animator.SetAnimationDuration(reloadSpeed);
            reloadTimer -= Time.deltaTime;
            if (0f >= reloadTimer) {
                currentAction = 0; // set to idle
            }
        }
    }


    void AttackTarget() {
        if ((0 == currentAction || 3 == currentAction)) { // if idle or attacking
            currentAction = 3; // set to attacking
            animator.UseAttackAnimations();
            animator.SetAnimationDuration(attackSpeed);
            if (animator.GetPoseTrigger() && 0f >= reloadTimer) {
                EmitProjectileParticles();
                if (null != attackAudio) {
                    attackAudio.Play();
                }
                reloadTimer = reloadSpeed;
            }
            if (animator.AnimationCompleted) {
                currentAction = 0; // set to idle
            }
        }
    }

    void AimWeapon() {
        transform.LookAt(target);
        if (0 == currentAction) { // if idle
            animator.UseAimAnimations();
        }
    }

    void DropTarget() {
        currentAction = 0;
        target = null;
        animator.UseIdleAnimations();
    }

    void EmitProjectileParticles() {
        foreach (ParticleSystem projectileParticles in projectileParticleSystems) {
            projectileParticles.Emit(1);
        }
    }
}
