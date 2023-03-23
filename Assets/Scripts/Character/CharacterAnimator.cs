using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{

    [SerializeField] CharacterAnimation currentAnimation;
    [SerializeField] Transform neck;
    [SerializeField] Transform upper;
    [SerializeField] Transform lower;

    [SerializeField] Transform rightShoulder;
    [SerializeField] Transform leftShoulder;

    [SerializeField] Transform rightElbow;
    [SerializeField] Transform leftElbow;

    [SerializeField] Transform rightWrist;
    [SerializeField] Transform leftWrist;

    [SerializeField] Transform rightHip;
    [SerializeField] Transform leftHip;

    void Update() {
        currentAnimation.gameObject.SetActive(true);
        ApplyPose(currentAnimation.CurrentPose);
    }

    void ApplyPose(AnimationPose pose) {
        if (null != pose) {
            neck.localEulerAngles = pose.NeckRotation;
            upper.localEulerAngles = pose.UpperRotation;
            lower.localEulerAngles = pose.LowerRotation;
            rightShoulder.localEulerAngles = pose.RightShoulderRotation;
            leftShoulder.localEulerAngles = pose.LeftShoulderRotation;
            rightElbow.localEulerAngles = pose.RightElbowRotation;
            leftElbow.localEulerAngles = pose.LeftElbowRotation;
            rightWrist.localEulerAngles = pose.RightWristRotation;
            leftWrist.localEulerAngles = pose.LeftWristRotation;
            rightHip.localEulerAngles = pose.RightHipRotation;
            leftHip.localEulerAngles = pose.LeftHipRotation;
        }
    }



}
