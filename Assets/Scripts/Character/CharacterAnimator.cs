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

    [Tooltip("Which animation list in the animation set is currrently active. 0 = Idle, 1 = Walk, 2 = Aim, 3 = Attack, 4 = Reload, 5 = Death, 6 = Special.")]
    [Range(0, 5)]
    [SerializeField] int currentAnimationList = 0;


    [Tooltip("If false, after the current animation has played a random animation from the same list will load.")]
    [SerializeField] bool lockAnimation = true;

    bool forceAnimationChange;

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
    }

    void OnEnable() {
        previousAnimationList = -1; // set previous animation list to negative to enable initial animation load
        forceAnimationChange = true; // load an animation at the start
        LoadAnimations();
        SetAnimation();
    }

    void Update() {
        PlayAnimation();
        LoadAnimations(); // check if new animations must be loaded
        if (animationCompleted) {
            SetAnimation(); // set animation and blend with previous, using bool flags to determine if a new animation should be laoded
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

    void SetAnimation() {
        if (null != animations                              // load a new animation if there is a loaded list
            && 0 < animations.Count                         // with 1 or more elements
            && (!lockAnimation || forceAnimationChange)) {  // the animation lock is false OR an animation change is being forced
            int index = Mathf.RoundToInt(Random.Range(0f, (float) animations.Count - 1));
            SetAnimation(animations[index]);
            forceAnimationChange = false; // should only be true for a single update after loading a new animation list
        } else {
             // default animation if there is no stored list
            SetAnimation(currentAnimation); 
        }
    }

    void SetAnimation(CharacterAnimation animation) {
        animationCompleted = false;
        animation.gameObject.SetActive(true);

        blendPose.SetPose(new AnimationPose.Pose(currentAnimation.CurrentPose)); // remove the blend pose if the animations are the same

        if (!currentAnimation.Equals(animation)) {   
            currentAnimation.gameObject.SetActive(false);
        }

        currentAnimation = animation;
    }

    // Load an animation list based on the currentAnimationsList int value. 0 = Idle, 1 = Walk, 2 = Aim, 3 = Attack, 4 = Reload, 5 = Death, 6 = Special."
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
                    case 6:
                        animations = animationSet.SpecialAnimations;
                        break;
                }
            }
            animationCompleted = true; // set to begin a new animation
            forceAnimationChange = true; // set so an animation from the set list will be loaded
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

    public void LoadSpecialAnimations() {
        currentAnimationList = 6;
        LoadAnimations();
    }

}
