using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitHealth))]
[RequireComponent(typeof(UnitMover))]
public class Unit : MonoBehaviour
{
    [SerializeField] int pointReward = 1;
    [SerializeField] int goldReward = 7;
    [SerializeField] int healthPenalty = 20;
    [SerializeField] GameObject penaltyFX;
    [SerializeField] GameObject deathFX;

    ScoreKeeper scoreKeeper;
    Bank bank;
    PlayerHealth playerHealth;
    RuntimeSpawns runtimeSpawns;
    CharacterAnimator animator;

    void Awake() {
        animator = transform.parent.GetComponentInChildren<CharacterAnimator>();

        GameObject player = GameObject.FindGameObjectWithTag(Player.playerTag);
        runtimeSpawns = GameObject.FindGameObjectWithTag(RuntimeSpawns.runtimeSpawnsTag)
            .GetComponent<RuntimeSpawns>();
        scoreKeeper = player.GetComponent<ScoreKeeper>(); 
        bank = player.GetComponent<Bank>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void OnEnable() {
        GetComponent<UnitMover>().BeginMoving();
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
    }

    public void PlayDeathAnimation() {
        animator.UseDeathAnimations();
        animator.SetAnimationSpeed(1f);
        animator.SetLooping(false);
    }

}
