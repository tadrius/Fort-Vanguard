using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationRig))]
public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] CharacterAnimation currentAnimation;

    AnimationRig animationRig;
    AnimationSet animationSet;

    void Awake() {
        animationRig = GetComponent<AnimationRig>();
        animationSet = GetComponentInChildren<AnimationSet>();
    }

    void Update() {
        if (null == currentAnimation) {
            LoadIdleAnimation();
        } 
        if (null != currentAnimation) {
            PlayAnimation();
        }
    }

    void PlayAnimation() {
        currentAnimation.gameObject.SetActive(true);
        animationRig.ApplyPose(currentAnimation.CurrentPose);
    }

    void LoadRandomAnimation(List<CharacterAnimation> animations) {
        if (animations.Count > 0) {
            int index = Mathf.RoundToInt(Random.Range(0f, (float) animations.Count - 1));
            LoadAnimation(animations[index]);
        }
    }

    void LoadAnimation(CharacterAnimation animation) {
        if (currentAnimation != animation) {
            currentAnimation.gameObject.SetActive(false);
        }
        currentAnimation = animation;
        currentAnimation.gameObject.SetActive(true);
    }

    public void LoadWalkAnimation() {
        LoadRandomAnimation(animationSet.WalkAnimations);
    }

    public void LoadIdleAnimation() {
        LoadRandomAnimation(animationSet.IdleAnimations);
    }

    public void LoadReloadAnimation() {
        LoadRandomAnimation(animationSet.ReloadAnimations);
    }

    public void LoadAttackAnimation() {
        LoadRandomAnimation(animationSet.AttackAnimations);        
    }

    public void LoadDeathAnimation() {
        LoadRandomAnimation(animationSet.DeathAnimations);
    }

}
