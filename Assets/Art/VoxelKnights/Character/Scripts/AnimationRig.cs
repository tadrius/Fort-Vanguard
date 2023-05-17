using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AnimationPose;

public class AnimationRig : MonoBehaviour
{
    [SerializeField] Transform neck;
    [SerializeField] Transform waist;
    [SerializeField] Transform ground;

    [SerializeField] Transform rightShoulder;
    [SerializeField] Transform leftShoulder;

    [SerializeField] Transform rightElbow;
    [SerializeField] Transform leftElbow;

    [SerializeField] Transform rightWrist;
    [SerializeField] Transform leftWrist;

    [SerializeField] Transform rightHip;
    [SerializeField] Transform leftHip;

    [SerializeField] Transform rightItem;
    [SerializeField] Transform leftItem;

    public void ApplyPose(AnimationPose.Pose pose) {
        if (null != pose) {
            neck.localEulerAngles = pose.neckRotation;
            waist.localEulerAngles = pose.waistRotation;
            ground.localEulerAngles = pose.groundRotation;
            rightShoulder.localEulerAngles = pose.rightShoulderRotation;
            leftShoulder.localEulerAngles = pose.leftShoulderRotation;
            rightElbow.localEulerAngles = pose.rightElbowRotation;
            leftElbow.localEulerAngles = pose.leftElbowRotation;
            rightWrist.localEulerAngles = pose.rightWristRotation;
            leftWrist.localEulerAngles = pose.leftWristRotation;
            rightHip.localEulerAngles = pose.rightHipRotation;
            leftHip.localEulerAngles = pose.leftHipRotation;
            rightItem.localEulerAngles = pose.rightItemRotation;
            leftItem.localEulerAngles = pose.leftItemRotation;
        }
    }

    public void ApplyZeroPose()
    {
        neck.localEulerAngles = Vector3.zero;
        waist.localEulerAngles = Vector3.zero;
        ground.localEulerAngles = Vector3.zero;
        rightShoulder.localEulerAngles = Vector3.zero;
        leftShoulder.localEulerAngles = Vector3.zero;
        rightElbow.localEulerAngles = Vector3.zero;
        leftElbow.localEulerAngles = Vector3.zero;
        rightWrist.localEulerAngles = Vector3.zero;
        leftWrist.localEulerAngles = Vector3.zero;
        rightHip.localEulerAngles = Vector3.zero;
        leftHip.localEulerAngles = Vector3.zero;
        rightItem.localEulerAngles = Vector3.zero;
        leftItem.localEulerAngles = Vector3.zero;
    }

}
