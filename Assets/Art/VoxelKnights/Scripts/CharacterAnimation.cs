using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    [SerializeField] List<AnimationPose> poses;

    AnimationPose.Pose currentPose = new AnimationPose.Pose(); // set to a new empty pose to avoid null exceptions
    int previousPoseIndex;
    float transitionProgress; // how much scaled time has elapsed transitioning from the current to next pose
    float transitionDuration;
    float totalDuration;
    bool poseTrigger;

    public bool useBlendPose;
    public AnimationPose.Pose CurrentPose { get { return currentPose; }}
    public float TotalDuration { get { return totalDuration; }}
    public float TransitionProgress { get { return transitionProgress; }}
    public bool PoseTrigger { get { return poseTrigger; }}

    void Awake() {
        ComputeTotalDuration();
        ResetAnimation();
    }

    public void ResetAnimation() {
        poseTrigger = false;
        previousPoseIndex = 0;
        transitionProgress = 0;
        AssignTransitionDuration();
        CreateCurrentPose(GetPreviousPose(), null); // initialize current pose to the first
    }

    void ComputeTotalDuration() {
        totalDuration = 0f;
        foreach (AnimationPose pose in poses) {
            totalDuration += pose.TransitionDuration;
        }
    }

    AnimationPose GetPreviousPose() {
        if (0 < poses.Count) { // if there is more than one pose
            return poses[previousPoseIndex];
        } else {
            return null;
        }
    }

    AnimationPose GetNextPose() {
        if (0 < poses.Count) {
            int nextPoseIndex = previousPoseIndex + 1;
            if (nextPoseIndex >= poses.Count) {
                return poses[0];
            }
            return poses[nextPoseIndex];
        } else {
            return null;
        }
    }

    // returns a bool indicating if the animation has finished
    public bool PlayAnimation(float animationProgress, AnimationRig rig, AnimationPose blendPose) {
        bool animationComplete = false;
        // if this is the start of the animation and blending is enabled, replace the first pose with the blend pose
        if (0 == previousPoseIndex && useBlendPose && null != blendPose.GetPose()) {
            CreateCurrentPose(blendPose, GetNextPose());
        } else {
            CreateCurrentPose(GetPreviousPose(), GetNextPose());
        }
        rig.ApplyPose(currentPose);

        transitionProgress += animationProgress;
        if (transitionProgress >= transitionDuration) {
            transitionProgress -= transitionDuration;
            previousPoseIndex++;
            if (previousPoseIndex == poses.Count - 1) { // if the previous pose is the last pose, mark the animation as complete
                animationComplete = true; 
            } else if (previousPoseIndex >= poses.Count) { // if the previous pose is after the last available, set back to the beginning
                previousPoseIndex = 0;
            }
            poseTrigger = GetPreviousPose().PoseTrigger; // only use pose's trigger when the pose is reached 
            AssignTransitionDuration();
        } else {
            poseTrigger = false; // reset to default false
        }

        return animationComplete;
    }

    void AssignTransitionDuration() {
        AnimationPose nextPose = GetNextPose();
        if (null != nextPose) {
            transitionDuration = nextPose.TransitionDuration;       
        } else {
            transitionDuration = float.MaxValue;
        }
    }

    void CreateCurrentPose(AnimationPose pose1, AnimationPose pose2) {
        currentPose = AnimationPose.CreateTransitionPose(pose1, pose2, transitionProgress/transitionDuration, transform);
    }

}
