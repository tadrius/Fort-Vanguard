using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPose : MonoBehaviour
{
    [SerializeField] Vector3 neckRotation = Vector3.zero;
    [SerializeField] Vector3 upperRotation = Vector3.zero;
    [SerializeField] Vector3 lowerRotation = Vector3.zero;

    [SerializeField] Vector3 rightShoulderRotation = Vector3.zero;
    [SerializeField] Vector3 leftShoulderRotation = Vector3.zero;

    [SerializeField] Vector3 rightElbowRotation = Vector3.zero;
    [SerializeField] Vector3 leftElbowRotation = Vector3.zero;

    [SerializeField] Vector3 rightWristRotation = Vector3.zero;
    [SerializeField] Vector3 leftWristRotation = Vector3.zero;

    [SerializeField] Vector3 rightHipRotation = Vector3.zero;
    [SerializeField] Vector3 leftHipRotation = Vector3.zero;

    public Vector3 NeckRotation { get { return neckRotation; }}
    public Vector3 UpperRotation { get { return upperRotation; }}
    public Vector3 LowerRotation { get { return lowerRotation; }}
    public Vector3 RightShoulderRotation { get { return rightShoulderRotation; }}
    public Vector3 LeftShoulderRotation { get { return leftShoulderRotation; }}
    public Vector3 RightElbowRotation { get { return rightElbowRotation; }}
    public Vector3 LeftElbowRotation { get { return leftElbowRotation; }}
    public Vector3 RightWristRotation { get { return rightWristRotation; }}
    public Vector3 LeftWristRotation { get { return leftWristRotation; }}
    public Vector3 RightHipRotation { get { return rightHipRotation; }}
    public Vector3 LeftHipRotation { get { return leftHipRotation; }}

    public static AnimationPose CreateTransitionPose(AnimationPose pose1, AnimationPose pose2, float transitionAmount, Transform parent) {
        AnimationPose transitionPose = GameObject.Instantiate<AnimationPose>(pose1, parent);

        transitionPose.neckRotation = Vector3.Lerp(pose1.neckRotation, pose2.neckRotation, transitionAmount);
        transitionPose.upperRotation = Vector3.Lerp(pose1.upperRotation, pose2.upperRotation, transitionAmount);
        transitionPose.lowerRotation = Vector3.Lerp(pose1.lowerRotation, pose2.lowerRotation, transitionAmount);
        transitionPose.rightShoulderRotation = Vector3.Lerp(pose1.rightShoulderRotation, pose2.rightShoulderRotation, transitionAmount);
        transitionPose.leftShoulderRotation = Vector3.Lerp(pose1.leftShoulderRotation, pose2.leftShoulderRotation, transitionAmount);
        transitionPose.rightElbowRotation = Vector3.Lerp(pose1.rightElbowRotation, pose2.rightElbowRotation, transitionAmount);
        transitionPose.leftElbowRotation = Vector3.Lerp(pose1.leftElbowRotation, pose2.leftElbowRotation, transitionAmount);
        transitionPose.rightWristRotation = Vector3.Lerp(pose1.rightWristRotation, pose2.rightWristRotation, transitionAmount);
        transitionPose.leftWristRotation = Vector3.Lerp(pose1.leftWristRotation, pose2.leftWristRotation, transitionAmount);
        transitionPose.rightHipRotation = Vector3.Lerp(pose1.rightHipRotation, pose2.rightHipRotation, transitionAmount);
        transitionPose.leftHipRotation = Vector3.Lerp(pose1.leftHipRotation, pose2.leftHipRotation, transitionAmount);

        return transitionPose;
    }

}
