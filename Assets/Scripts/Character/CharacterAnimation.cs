using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    [SerializeField] List<AnimationPose> poses;

    AnimationPose.Pose currentPose;
    int previousPoseIndex = 0;
    float transitionProgress = 0f; // how much scaled time has elapsed transitioning from the current to next pose
    float transitionDuration;
    float totalDuration;

    public AnimationPose.Pose CurrentPose { get { return currentPose; }}
    public float TotalDuration { get { return totalDuration; }}

    void Awake() {
        CreateCurrentPose();
        ComputeTotalDuration();
    }

    void ComputeTotalDuration() {
        totalDuration = 0f;
        foreach (AnimationPose pose in poses) {
            totalDuration += pose.TransitionDuration;
        }
    }

    public AnimationPose GetPreviousPose() {
        return poses[previousPoseIndex];
    }

    public AnimationPose GetNextPose() {
        if (previousPoseIndex + 1 >= poses.Count) {
            return poses[0];
        }
        return poses[previousPoseIndex + 1];
    }

    // returns if the animation has finished
    public bool PlayAnimation(float animationProgress, AnimationRig rig) {
        bool animationComplete = false;
        transitionProgress += animationProgress;
        if (transitionProgress >= transitionDuration) {
            transitionProgress = transitionProgress - transitionDuration;
            previousPoseIndex++;
            if (previousPoseIndex >= poses.Count) { // reset animation
                previousPoseIndex = 0;
                animationComplete = true;
            }
        }
        CreateCurrentPose();
        rig.ApplyPose(currentPose);
        return animationComplete;
    }

    void CreateCurrentPose() {
        currentPose = AnimationPose.CreateTransitionPose(GetPreviousPose(), GetNextPose(), transitionProgress/transitionDuration, transform);
        transitionDuration = GetPreviousPose().TransitionDuration;
    }

}
