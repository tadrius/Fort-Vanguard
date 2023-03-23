using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] CharacterAnimation currentAnimation;

    AnimationSet animationSet;
    AnimationRig animationRig;

    void Awake() {
        animationSet = GetComponentInChildren<AnimationSet>();
        animationRig = GetComponentInChildren<AnimationRig>();
    }

    void Update() {
        PlayAnimation(currentAnimation);
        ApplyPose(currentAnimation.CurrentPose);
    }

    void PlayRandomAnimation(List<CharacterAnimation> animations) {
        if (animations.Count > 0) {
            int index = Mathf.RoundToInt(Random.Range(0f, (float) animations.Count - 1));
            PlayAnimation(animations[index]);
        }
    }

    void PlayAnimation(CharacterAnimation animation) {
        if (currentAnimation != animation) {
            currentAnimation.gameObject.SetActive(false);
        }
        currentAnimation = animation;
        currentAnimation.gameObject.SetActive(true);
        ApplyPose(currentAnimation.CurrentPose);
    }

    void ApplyPose(AnimationPose.Pose pose) {
        animationRig.ApplyPose(pose);
    }

    public void PlayWalkAnimation() {
        PlayRandomAnimation(animationSet.WalkAnimations);
    }

    public void PlayIdleAnimation() {
        PlayRandomAnimation(animationSet.IdleAnimations);
    }

    public void PlayReloadAnimation() {
        PlayRandomAnimation(animationSet.ReloadAnimations);
    }

    public void PlayAttackAnimation() {
        PlayRandomAnimation(animationSet.AttackAnimations);        
    }

    public void PlayDeathAnimation() {
        PlayRandomAnimation(animationSet.DeathAnimations);
    }

}
