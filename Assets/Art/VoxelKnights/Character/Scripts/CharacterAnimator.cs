using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimationRig))]
[RequireComponent(typeof(AnimationPose))]
public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] CharacterAnimation currentAnimation;
    [Tooltip("Animations will be scaled to play within the desired animation duration (when speed is set to 1). Set to 0 to remove scaling.")]
    [SerializeField] float animationDuration = 0f;

    [Tooltip("How quickly the animation will play after it has been scaled to the animation duration.")]
    [SerializeField] float animationSpeed = 1f;

    [Tooltip("Which animation list in the animation set is currrently active. 0 = Idle, 1 = Walk, 2 = Aim, 3 = Attack, 4 = Reload, 5 = Death, 6 = Special.")]
    [Range(0, 6)]
    [SerializeField] int currentAnimationList = 0;


    [Tooltip("If false, after the current animation has played a random animation from the same list will load.")]
    [SerializeField] bool lockAnimation = true;

    [Tooltip("If true, animations will play one after another.")]
    [SerializeField] bool looping = true;

    bool forceAnimationChange;

    int previousAnimationList;

    List<CharacterAnimation> animations;
    AnimationRig animationRig;
    AnimationSet animationSet;

    // for transitioning from one animation to another
    AnimationPose blendPose;
    bool animationCompleted;
    float excessProgress;

    public bool AnimationCompleted { get { return animationCompleted; }}

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
        LoadAnimations(); // check if new animations must be loaded
        if (animationCompleted || forceAnimationChange) {
            SetAnimation(); // set animation and blend with previous, using bool flags to determine if a new animation should be laoded
        } 
        if (!animationCompleted) {
            PlayAnimation();
        }
    }

    // return whether or not the animation was completed
    void PlayAnimation() {

        // scale time to play animation to the desired duration
        float timeScaler = 1f;
        if (0f != animationDuration) {
            timeScaler = currentAnimation.TotalDuration / animationDuration;
        }

        // multiply the time scaler by the animation speed
        timeScaler *= animationSpeed;

        // play the animation
        animationCompleted = currentAnimation.PlayAnimation(Time.deltaTime * timeScaler, animationRig, blendPose);
    }

    void SetAnimation() {
        if (null != animations                              // load a new animation if there is a loaded list
                && 0 < animations.Count                         // with 1 or more elements
                && (!lockAnimation || forceAnimationChange)) {  // the animation lock is false OR an animation change is being forced

            int index = Mathf.RoundToInt(Random.Range(0f, (float) animations.Count - 1));
            SetAnimation(animations[index]);
        } else {
            SetAnimation(currentAnimation); 
        }
    }

    void SetAnimation(CharacterAnimation animation) {
        animationCompleted = !(forceAnimationChange || looping); // mark animation completed false if looping or force animation change is enabled
        forceAnimationChange = false; // should only be true for a single update after loading a new animation list

        if (!currentAnimation.Equals(animation)) {
            animation.gameObject.SetActive(true);
            animation.ResetAnimation();
            blendPose.SetPose(new AnimationPose.Pose(currentAnimation.CurrentPose)); // set the blend pose to the current pose before switching to a new animation 
            currentAnimation.gameObject.SetActive(false);
            currentAnimation = animation;
            currentAnimation.useBlendPose = true;
        } else {
            currentAnimation.useBlendPose = false;
        }
    }

    public void SetAnimationDuration(float duration) {
        animationDuration = duration;
    }

    public void SetAnimationSpeed(float speed) {
        animationSpeed = speed;
    }

    public void SetLooping(bool looping) {
        this.looping = looping;
    }

    public bool GetPoseTrigger() {
        return currentAnimation.PoseTrigger;
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
            forceAnimationChange = true; // set so an animation from the set list will be loaded instantly
        }
    }

    public void UseIdleAnimations() {
        currentAnimationList = 0;
    }

    public void UseWalkAnimations() {
        currentAnimationList = 1;
    }

    public void UseAimAnimations() {
        currentAnimationList = 2;
    }

    public void UseAttackAnimations() {
        currentAnimationList = 3;
    }

    public void UseReloadAnimations() {
        currentAnimationList = 4;
    }

    public void UseDeathAnimations() {
        currentAnimationList = 5;
    }

    public void UseSpecialAnimations() {
        currentAnimationList = 6;
    }

}
