using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{

    [Tooltip("The maximum range at which an enemy will be targeted.")]
    [SerializeField] float range = 50f;
    [Tooltip("How much time must pass between each attack animation. Set to 0 to use the default animation length.")]
    [SerializeField] float reloadSpeed = 1f;
    [Tooltip("How quickly the attack animation plays. Set to 0 to use the default animation length.")]
    [SerializeField] float attackSpeed = 0f;

    CharacterAnimator characterAnimator;
    AudioSource attackAudio;
    ParticleSystem[] attackParticleSystems;

    Transform target;
    Building building;

    int currentAction = 0; // 0 = Idle, 1 = Walk, 2 = Aim, 3 = Attack, 4 = Reload, 5 = Death, 6 = Special

    float reloadTimer = 0f;

    void Awake() {
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        attackAudio = GetComponentInChildren<AudioSource>();
        attackParticleSystems = GetComponentsInChildren<ParticleSystem>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
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
            characterAnimator.UseReloadAnimations();
            characterAnimator.SetAnimationDuration(reloadSpeed);
            reloadTimer -= Time.deltaTime;
            if (0f >= reloadTimer) {
                currentAction = 0; // set to idle
            }
        }
    }


    void AttackTarget() {
        if ((0 == currentAction || 3 == currentAction)) { // if idle or attacking
            currentAction = 3; // set to attacking
            characterAnimator.UseAttackAnimations();
            characterAnimator.SetAnimationDuration(attackSpeed);
            if (characterAnimator.GetPoseTrigger() && 0f >= reloadTimer) {
                EmitProjectileParticles();
                if (null != attackAudio) {
                    attackAudio.Play();
                }
                reloadTimer = reloadSpeed;
            }
            if (characterAnimator.AnimationCompleted) {
                currentAction = 0; // set to idle
            }
        }
    }

    void AimWeapon() {
        Vector3 levelTargetPosition = target.position; // the target's x and z positions and the attacker's y position.
        levelTargetPosition.y = transform.position.y;

        transform.LookAt(levelTargetPosition);
        foreach (ParticleSystem pSystem in attackParticleSystems) {
            pSystem.transform.LookAt(target);
        }
        if (0 == currentAction) { // if idle
            characterAnimator.UseAimAnimations();
        }
    }

    void DropTarget() {
        currentAction = 0;
        target = null;
        characterAnimator.UseIdleAnimations();
    }

    void EmitProjectileParticles() {
        foreach (ParticleSystem pSystem in attackParticleSystems) {
            pSystem.Emit(1);
        }
    }
}
