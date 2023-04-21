using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] int maxHitPoints = 10;
    [SerializeField] [Range(0f, 10f)] float speed = 1f;
    [SerializeField] int pointReward = 1;
    [SerializeField] int goldReward = 7;
    [SerializeField] int healthPenalty = 20;
    [SerializeField] ParticleSystem damageParticles;
    [SerializeField] GameObject penaltyFX;
    [SerializeField] GameObject deathFX;

    // child components
    CharacterAnimator animator;
    UnitHealth unitHealth;
    UnitMover unitMover;

    // external object components
    ScoreKeeper scoreKeeper;
    Bank bank;
    PlayerHealth playerHealth;
    RuntimeSpawns runtimeSpawns;

    public CharacterAnimator Animator { get { return animator; }}
    public ParticleSystem DamageParticles { get { return damageParticles; }}

    void Awake() {
        animator = GetComponentInChildren<CharacterAnimator>(true);
        unitHealth = GetComponentInChildren<UnitHealth>(true);
        unitMover = GetComponentInChildren<UnitMover>(true);

        GameObject player = GameObject.FindGameObjectWithTag(Player.playerTag);
        runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag).GetComponent<RuntimeSpawns>();
        scoreKeeper = player.GetComponent<ScoreKeeper>(); 
        bank = player.GetComponent<Bank>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnEnable() {
        unitHealth.gameObject.SetActive(true); // should be the same game object as unit mover

        unitHealth.SetHitPoints(maxHitPoints);
        unitMover.BeginMoving(speed);
    }

    public void DepositReward() {
        if (null != bank) {
            bank.Deposit(goldReward);
        }
        if (null != scoreKeeper) {
            scoreKeeper.AddToScore(pointReward);
        }
    }

    public void IncurPenalty() {
        if (null != playerHealth) {
            playerHealth.Damage(healthPenalty);
        }
    }

    // FX Methods
    public void SpawnDeathFX() {
        runtimeSpawns.SpawnObject(deathFX, transform.position);
    }

    public void SpawnPenaltyFX() {
        runtimeSpawns.SpawnObject(penaltyFX, transform.position);
    }

    // Animation Methods
    public void PlayWalkAnimation(float animationSpeed) {
        animator.UseWalkAnimations();
        animator.SetAnimationSpeed(animationSpeed);
        animator.SetLooping(true);
    }

    public void PlayDeathAnimation() {
        animator.UseDeathAnimations();
        animator.SetAnimationSpeed(1f);
        animator.SetLooping(false);
    }

}
