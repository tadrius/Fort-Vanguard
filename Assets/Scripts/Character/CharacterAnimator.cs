using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationRig))]
[RequireComponent(typeof(AnimationPose))]
public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] CharacterAnimation currentAnimation;
    [Tooltip("All animations will be scaled to play within the desired animation duration. Set to 0 to remove scaling.")]
    [SerializeField] float desiredAnimationDuration = 0f;

    [Tooltip("Which animation list in the animation set is currrently active. 0 = Idle, 1 = Walk, 2 = Aim, 3 = Attack, 4 = Reload, 5 = Death.")]
    [Range(0, 5)]
    [SerializeField] int currentAnimationList = 0;
    int previousAnimationList;

    List<CharacterAnimation> animations;
    AnimationRig animationRig;
    AnimationSet animationSet;

    // for transitioning from one animation to another
    AnimationPose blendPose;
    bool animationCompleted;
    float excessProgress;

    void Awake() {
        animationRig = GetComponent<AnimationRig>();
        animationSet = GetComponentInChildren<AnimationSet>();
        blendPose = GetComponent<AnimationPose>();
        previousAnimationList = -1; // set previous animation list to negative to enable initial animation load
        LoadAnimations();
    }

    void Update() {
        LoadAnimations();
        if (null != currentAnimation) {
            PlayAnimation();
            if (animationCompleted) { 
                SetRandomAnimation(); // set another animation from the stored list and blend
            }
        }
    }

    // return whether or not the animation was completed
    void PlayAnimation() {
        // scale time to play animation to the desired duration
        float timeScaler = 1f;
        if (0f != desiredAnimationDuration) {
            timeScaler = currentAnimation.TotalDuration / desiredAnimationDuration;
        }

        // play the animation
        animationCompleted = currentAnimation.PlayAnimation(Time.deltaTime * timeScaler, animationRig, blendPose);
    }

    void SetRandomAnimation() {
        if (null != animations && 0 < animations.Count) {
            int index = Mathf.RoundToInt(Random.Range(0f, (float) animations.Count - 1));
            SetAnimation(animations[index]);
        }
    }

    void SetAnimation(CharacterAnimation animation) {
        animationCompleted = false;
        animation.gameObject.SetActive(true);

        animation.SetTransitionProgress(currentAnimation.TransitionProgress); // apply the excess progress to the next animation
        blendPose.SetPose(new AnimationPose.Pose(currentAnimation.CurrentPose)); // remove the blend pose if the animations are the same

        if (!currentAnimation.Equals(animation)) {   
            currentAnimation.gameObject.SetActive(false);
        }

        currentAnimation = animation;
    }

    // Load an animation list based on the currentAnimationsList int value. 0 = Idle, 1 = Walk, 2 = Aim, 3 = Attack, 4 = Reload, 5 = Death."
    void LoadAnimations() {
        if (previousAnimationList != currentAnimationList) { // only perform the following if the current list has changed
            previousAnimationList = currentAnimationList;
            if (null != animationSet) {
                switch (currentAnimationList) {
                    case 0:
                        animations = animationSet.IdleAnimations;
                        break;
                    case 1:
                        animations = animationSet.WalkAnimations;
                        break;
                    case 2:
                        animations = animationSet.AimAnimations;
                        break;
                    case 3:
                        animations = animationSet.AttackAnimations;
                        break;
                    case 4:
                        animations = animationSet.ReloadAnimations;
                        break;
                    case 5:
                        animations = animationSet.DeathAnimations;
                        break;
                }
            }
            if (null != animations && 0 < animations.Count) { // animation from stored list
                SetRandomAnimation();
            } else { // default animation if there is no stored list
                SetAnimation(currentAnimation); 
            }
        }
    }

    public void LoadIdleAnimations() {
        currentAnimationList = 0;
        LoadAnimations();
    }

    public void LoadWalkAnimations() {
        currentAnimationList = 1;
        LoadAnimations();
    }

    public void LoadAimAnimations() {
        currentAnimationList = 2;
        LoadAnimations();
    }

    public void LoadAttackAnimations() {
        currentAnimationList = 3;
        LoadAnimations();
    }

    public void LoadReloadAnimations() {
        currentAnimationList = 4;
        LoadAnimations();
    }

    public void LoadDeathAnimations() {
        currentAnimationList = 5;
        LoadAnimations();
    }

}
