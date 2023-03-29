using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationRig))]
public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] CharacterAnimation currentAnimation;
    [SerializeField] List<CharacterAnimation> animations;

    AnimationRig animationRig;
    AnimationSet animationSet;

    void Awake() {
        animationRig = GetComponent<AnimationRig>();
        animationSet = GetComponentInChildren<AnimationSet>();
        if (null != animationSet) {
            LoadAttackAnimations(); // TODO - remove hard-coded animations
            SetRandomAnimation(animations);
        }
    }

    void Update() {
        if (null != currentAnimation) {
            bool animComplete = PlayAnimation();
            if (animComplete) { // start a different animation from the same list
                SetRandomAnimation(animations);
            }
        }
    }

    bool PlayAnimation() {
        currentAnimation.gameObject.SetActive(true);
        animationRig.ApplyPose(currentAnimation.CurrentPose);
        return currentAnimation.PlayAnimation();
    }

    void SetRandomAnimation(List<CharacterAnimation> animations) {
        if (animations.Count > 0) {
            int index = Mathf.RoundToInt(Random.Range(0f, (float) animations.Count - 1));
            SetAnimation(animations[index]);
        }
    }

    void SetAnimation(CharacterAnimation animation) {
        if (currentAnimation != animation) {
            currentAnimation.gameObject.SetActive(false);
        }
        currentAnimation = animation;
        currentAnimation.gameObject.SetActive(true);
    }

    public void LoadWalkAnimations() {
        animations = animationSet.WalkAnimations;
    }

    public void LoadIdleAnimations() {
        animations = animationSet.IdleAnimations;
    }

    public void LoadReloadAnimations() {
        animations = animationSet.ReloadAnimations;
    }

    public void LoadAttackAnimations() {
        animations = animationSet.AttackAnimations;
    }

    public void LoadDeathAnimations() {
        animations = animationSet.DeathAnimations;
    }

}
