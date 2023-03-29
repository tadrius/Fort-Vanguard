using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    [SerializeField] int previousPoseIndex = 0; // indicates the starting pose of the animation
    [SerializeField] float transitionDuration = .25f;
    [SerializeField] List<AnimationPose> poses;
    [SerializeField] float transitionProgress = 0f; // how much time has elapsed transitioning from the current to next pose

    AnimationPose.Pose currentPose;
    public AnimationPose.Pose CurrentPose { get { return currentPose; }}

    void Awake() {
        CreateCurrentPose();
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
    public bool PlayAnimation() {
        bool animationComplete = false;
        transitionProgress += Time.deltaTime;
        if (transitionProgress >= transitionDuration) {
            transitionProgress = transitionProgress - transitionDuration;
            previousPoseIndex++;
            if (previousPoseIndex >= poses.Count) { // reset animaation
                previousPoseIndex = 0;
                animationComplete = true;
            }
        }
        CreateCurrentPose();
        return animationComplete;
    }

    void CreateCurrentPose() {
        currentPose = AnimationPose.CreateTransitionPose(GetPreviousPose(), GetNextPose(), transitionProgress/transitionDuration, transform);
    }

}
