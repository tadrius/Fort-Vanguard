using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    float reloadMultiplier = 1f;   // A multiplier on the reload speed, allowing for buffs and debuffs.
    bool buffsChanged = false;
    Dictionary<Buffer, Buff> buffsBySource = new Dictionary<Buffer, Buff>();
    Transform target;
    Building building;
    WaveManager waveManager;

    bool reloadRequired;
    Action currentAction;
    enum Action { Idle, Walk, Aim, Attack, Reload, Death, Special };

    public bool BuffsChanged { get {  return buffsChanged; } set {  buffsChanged = value; } }

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
        if (buffsChanged) {
            UpdateBuffEffects();
        }
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

    public Buff AddBuff(Buffer buffer)
    {
        if (buffsBySource.TryAdd(buffer, buffer.Buff))  // try adding the buffer with a placeholder to the buffs dictionary.
        {
            Buff buff = Instantiate(buffer.Buff, transform); // if this works, create an instance of the buff
            buffsBySource[buffer] = buff; // replace the placeholder with the newly created buff 
            buffsChanged = true;
            return buff;
        }
        return null;
    }

    void UpdateBuffEffects()
    {
        float reloadMultiplier = 1f;
        foreach (KeyValuePair<Buffer, Buff> bufferToBuff in buffsBySource.ToList())
        {
            Buffer buffer = bufferToBuff.Key;
            if (null != buffer && buffer.InRange(this))
            {
                reloadMultiplier *= bufferToBuff.Value.ReloadMultiplier;
            } else
            {
                buffsBySource.Remove(bufferToBuff.Key);
            }
        }
        this.reloadMultiplier = reloadMultiplier;
    }

    void FindClosestTarget() {
        Transform closestTarget = null;
        float maxDistance = range;

        Wave wave = waveManager.Waves[waveManager.CurrentWaveIndex]; // only need to consider at enemies in current wave
        if (null != wave && null != wave.SpawnedUnits) {
            foreach (Unit un in wave.SpawnedUnits) {
                if (un.isActiveAndEnabled) {
                    float distance = Vector3.Distance(transform.position, un.transform.position);
                    if (distance < maxDistance) {
                        closestTarget = un.transform;
                        maxDistance = distance;
                    }
                }
            }
            target = closestTarget;
        }
    }

    bool TargetIsValid() {
        if (null != target && target.GetComponent<Unit>().isActiveAndEnabled) {
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
        yield return new WaitForSeconds(reloadSpeed * reloadMultiplier);
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
            animator.SetAnimationDuration(reloadSpeed * reloadMultiplier);
        }
    }

}
