using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    [SerializeField] List<AnimationPose> poses;

    [SerializeField] AnimationPose.Pose currentPose;

    int previousPoseIndex;
    float transitionProgress; // how much scaled time has elapsed transitioning from the current to next pose
    float transitionDuration;
    float totalDuration;

    public AnimationPose.Pose CurrentPose { get { return currentPose; }}
    public float TotalDuration { get { return totalDuration; }}
    public float TransitionProgress { get { return transitionProgress; }}

    void Awake() {
        transitionDuration = GetPreviousPose().TransitionDuration;
        ComputeTotalDuration();
    }

    void OnEnable() {
        ResetAnimation();
    }

    void ResetAnimation() {
        previousPoseIndex = 0;
        transitionProgress = 0;
        currentPose = GetPreviousPose().GetPose(); // initialize current pose to the first
    }

    void ComputeTotalDuration() {
        totalDuration = 0f;
        foreach (AnimationPose pose in poses) {
            totalDuration += pose.TransitionDuration;
        }
    }

    public void SetTransitionProgress(float progress) {
        transitionProgress = progress;
    }

    public void SetPreviousPoseIndex(int index) {
        previousPoseIndex = index;
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

    // returns a bool indicating if the animation has finished
    public bool PlayAnimation(float animationProgress, AnimationRig rig, AnimationPose blendPose) {
        bool animationComplete = false;
        // if this is the first pose in the sequence, replace the pose with the animation blend pose
        if (0 == previousPoseIndex && null != blendPose.GetPose()) {
            CreateCurrentPose(blendPose, GetNextPose());
        } else {
            CreateCurrentPose(GetPreviousPose(), GetNextPose());
        }
        rig.ApplyPose(currentPose);

        transitionProgress += animationProgress;
        if (transitionProgress >= transitionDuration) {
            transitionProgress -= transitionDuration;
            previousPoseIndex++;
            if (previousPoseIndex >= poses.Count) { // reset animation
                previousPoseIndex = 0;
                animationComplete = true;
                transitionDuration = GetPreviousPose().TransitionDuration;
            }
        }

        return animationComplete;
    }

    void CreateCurrentPose(AnimationPose pose1, AnimationPose pose2) {
        currentPose = AnimationPose.CreateTransitionPose(pose1, pose2, transitionProgress/transitionDuration, transform);
    }

}
