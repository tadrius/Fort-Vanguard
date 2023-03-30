using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationPose : MonoBehaviour
{
    [Tooltip("The joint rotations that define this pose.")]
    [SerializeField] Pose pose;

    [Tooltip("The amount of time it takes to transition from the previous pose into this current pose.")]
    [SerializeField] float transitionDuration = .3f;
    
    public float TransitionDuration { get { return transitionDuration; } }

    public static Pose CreateTransitionPose(AnimationPose animPose1, AnimationPose animPose2, float transitionAmount, Transform parent) {
        Pose pose = new Pose();
        Pose pose1 = animPose1.pose;
        Pose pose2 = animPose2.pose;

        pose.neckRotation = Vector3.Lerp(pose1.neckRotation, pose2.neckRotation, transitionAmount);
        pose.upperRotation = Vector3.Lerp(pose1.upperRotation, pose2.upperRotation, transitionAmount);
        pose.lowerRotation = Vector3.Lerp(pose1.lowerRotation, pose2.lowerRotation, transitionAmount);
        pose.rightShoulderRotation = Vector3.Lerp(pose1.rightShoulderRotation, pose2.rightShoulderRotation, transitionAmount);
        pose.leftShoulderRotation = Vector3.Lerp(pose1.leftShoulderRotation, pose2.leftShoulderRotation, transitionAmount);
        pose.rightElbowRotation = Vector3.Lerp(pose1.rightElbowRotation, pose2.rightElbowRotation, transitionAmount);
        pose.leftElbowRotation = Vector3.Lerp(pose1.leftElbowRotation, pose2.leftElbowRotation, transitionAmount);
        pose.rightWristRotation = Vector3.Lerp(pose1.rightWristRotation, pose2.rightWristRotation, transitionAmount);
        pose.leftWristRotation = Vector3.Lerp(pose1.leftWristRotation, pose2.leftWristRotation, transitionAmount);
        pose.rightHipRotation = Vector3.Lerp(pose1.rightHipRotation, pose2.rightHipRotation, transitionAmount);
        pose.leftHipRotation = Vector3.Lerp(pose1.leftHipRotation, pose2.leftHipRotation, transitionAmount);

        return pose;
    }

    [System.Serializable]
    public class Pose {
        public Vector3 neckRotation = Vector3.zero;
        public Vector3 upperRotation = Vector3.zero;
        public Vector3 lowerRotation = Vector3.zero;

        public Vector3 rightShoulderRotation = Vector3.zero;
        public Vector3 leftShoulderRotation = Vector3.zero;

        public Vector3 rightElbowRotation = Vector3.zero;
        public Vector3 leftElbowRotation = Vector3.zero;

        public Vector3 rightWristRotation = Vector3.zero;
        public Vector3 leftWristRotation = Vector3.zero;

        public Vector3 rightHipRotation = Vector3.zero;
        public Vector3 leftHipRotation = Vector3.zero;      
    }

}
