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

    CharacterAnimator animator;
    AudioSource attackAudio;
    ParticleSystem projectileParticles;

    Transform target;
    Building building;
    WaveManager waveManager;

    bool reloadRequired;
    Action currentAction;
    enum Action { Idle, Walk, Aim, Attack, Reload, Death, Special };


    void Awake() {
        animator = GetComponentInChildren<CharacterAnimator>();
        attackAudio = GetComponentInChildren<AudioSource>();
        projectileParticles = GetComponentInChildren<ParticleSystem>();
        waveManager = FindObjectOfType<WaveManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        building = GetComponentInParent<Building>();
        if (building.IsElevated) {
            range *= 2;
        }
        reloadRequired = false;
        currentAction = Action.Idle;
        UseIdleAnimations();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentAction) {
            case Action.Idle:
                FindClosestTarget();
                AttemptAim();
                break;
            case Action.Aim:
                PointWeapon();
                StartCoroutine(StartAimSequence());
                break;
            case Action.Attack:
                PointWeapon();
                break;
            case Action.Reload:
                PointWeapon();
                break;
        }
    }

    void FindClosestTarget() {
        Transform closestTarget = null;
        float maxDistance = range;

        Wave wave = waveManager.Waves[waveManager.CurrentWaveIndex]; // only need to consider at enemies in current wave
        if (null != wave) {
            foreach (Unit en in wave.SpawnedEnemies) {
                if (null != en) {
                    float enemyDistance = Vector3.Distance(transform.position, en.transform.position);
                    if (en.gameObject.activeSelf && enemyDistance < maxDistance) {
                        closestTarget = en.transform;
                        maxDistance = enemyDistance;
                    }
                }
            }
            target = closestTarget;
        }
    }

    bool TargetIsValid() {
        if (null != target && target.gameObject.activeSelf) {
            float targetDistance = Vector3.Distance(transform.position, target.position);
            if (range >= targetDistance) {
                return true;
            }
        }
        return false;
    }

    void DropTarget() {
        currentAction = Action.Idle;
        UseIdleAnimations();
        target = null;
    }

    void AttemptAim() {
        if (TargetIsValid()) {
            UseAimAnimations();
            currentAction = Action.Aim;
        } else {
            DropTarget();
        }
    }

    IEnumerator StartAimSequence() {
        yield return StartCoroutine(Attack());
        if (reloadRequired) {
            yield return StartCoroutine(Reload());
        }
        AttemptAim();
    }

    IEnumerator Reload() {
        currentAction = Action.Reload;
        UseReloadAnimation();
        yield return new WaitForSeconds(reloadSpeed);
        reloadRequired = false;
    }

    IEnumerator Attack() {
        currentAction = Action.Attack;
        UseAttackAnimations();
        yield return new WaitUntil(() => animator.GetPoseTrigger());
        
        if (TargetIsValid()) {
            projectileParticles.Emit(1);
            if (null != attackAudio) {
                attackAudio.Play();
            }
            yield return new WaitUntil(() => animator.AnimationCompleted);
            reloadRequired = true;
        } else {
            DropTarget();
        }
    }

    void PointWeapon() {
        if (null != target) {
            Vector3 levelTargetPosition = target.position; // use the target's x and z positions and the attacker's y position.
            levelTargetPosition.y = transform.position.y;

            transform.LookAt(levelTargetPosition);
            projectileParticles.transform.LookAt(target);
        }
    }

    void UseIdleAnimations() {
        animator.UseIdleAnimations();
        animator.SetAnimationDuration(0f);
    }

    void UseAimAnimations() {
        animator.UseAimAnimations();
        animator.SetAnimationDuration(0f);
    }

    void UseAttackAnimations() {
        animator.UseAttackAnimations();
        animator.SetAnimationDuration(attackSpeed);
    }

    void UseReloadAnimation() {
        int animationCount = animator.UseReloadAnimations();
        if (0 >= animationCount) { // if the animator has no reload animations use the aim animation
            UseAimAnimations();
        } else {
            animator.SetAnimationDuration(reloadSpeed);
        }
    }

}
