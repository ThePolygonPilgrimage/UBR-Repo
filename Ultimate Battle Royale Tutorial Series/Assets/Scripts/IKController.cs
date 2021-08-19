using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKController : MonoBehaviour
{
    public static IKController instance;

    Animator anim;

    [Header("Look At Object")]
    [Range(0, 1)] public float lookAtWeight;
    public Transform lookAtObj = null;

    [Header("Right Hand Grip")]
    [Range(0, 1)] public float RightIKWeight;
    public Transform rightHandObj = null;
    [Range(0, 1)] public float rightHintWeight;
    public Transform rightHandHint = null;

    [Header("Left Hand Grip")]
    [Range(0, 1)] public float LeftIKWeight;
    public Transform leftHandObj = null;
    [Range(0, 1)] public float leftHintWeight;
    public Transform leftHandHint = null;

    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
        if(anim)
        {
            if (lookAtObj != null)
            {
                anim.SetLookAtWeight(lookAtWeight);
                anim.SetLookAtPosition(lookAtObj.position);
            }

            if (rightHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.RightHand, RightIKWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.RightHand, RightIKWeight);
                anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            }

            if (rightHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, rightHintWeight);
                anim.SetIKHintPosition(AvatarIKHint.RightElbow, rightHandHint.position);
            }

            if (leftHandObj != null)
            {
                anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, LeftIKWeight);
                anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, LeftIKWeight);
                anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            }

            if (leftHandHint != null)
            {
                anim.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, leftHintWeight);
                anim.SetIKHintPosition(AvatarIKHint.LeftElbow, leftHandHint.position);
            }
        }
    }
}
