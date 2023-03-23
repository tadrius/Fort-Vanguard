using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRig : MonoBehaviour
{
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

    public void ApplyPose(AnimationPose.Pose pose) {
        if (null != pose) {
            neck.localEulerAngles = pose.neckRotation;
            upper.localEulerAngles = pose.upperRotation;
            lower.localEulerAngles = pose.lowerRotation;
            rightShoulder.localEulerAngles = pose.rightShoulderRotation;
            leftShoulder.localEulerAngles = pose.leftShoulderRotation;
            rightElbow.localEulerAngles = pose.rightElbowRotation;
            leftElbow.localEulerAngles = pose.leftElbowRotation;
            rightWrist.localEulerAngles = pose.rightWristRotation;
            leftWrist.localEulerAngles = pose.leftWristRotation;
            rightHip.localEulerAngles = pose.rightHipRotation;
            leftHip.localEulerAngles = pose.leftHipRotation;
        }
    }

}
