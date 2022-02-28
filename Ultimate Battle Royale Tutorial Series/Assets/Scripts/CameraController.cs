using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    [Header("Follow Targets")]
    [SerializeField] Transform followTarget;
    [SerializeField] Transform followCamTarget;

    [Header("Camera Angles")]
    [SerializeField] Vector2 rotSpeed = new Vector2(2.5f, 2.5f);
    [SerializeField] Vector2 pitchClamp = new Vector2(70f, -11.5f);

    [Header("Camera Movement")]
    [SerializeField] [Range(0, 100)] float turnSpeed = 15f;
    [SerializeField] [Range(0, 100)] float dampTime = 0.07f;
    [Range(0, 100)] public float maxCamDistance = 3f;
    [SerializeField] [Range(0, 100)] float minCamDistance = 1f;
    Vector2 newRotation;
    Vector2 rotation;
    Vector3 dampVelocity;

    [Header("Clipping Detection")]
    [SerializeField] [Range(0, 100)] float camCollisionDistance = 2f;
    [SerializeField] [Range(0, 100)] float camCollisionDampRate = 10f;
    [SerializeField] [Range(0, 100)] float camCollisionReturnDampTime = 0.16f;
    [SerializeField] LayerMask notPlayerMask;
    [SerializeField] [Range(0, 100)] float currentCamDistance;
    [SerializeField] [Range(0, 100)] float collisionDampVelocity;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    //void /*Fixed*/Update()
    //{
    //    //float yawCamera = followCamTarget.transform.rotation.eulerAngles.y;
    //    //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
    //}

    void LateUpdate()
    {
        //Added
        float yawCamera = followCamTarget.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yawCamera, 0), turnSpeed * Time.fixedDeltaTime);
        //Added

        newRotation.y += Input.GetAxis("Mouse X") * rotSpeed.y;
        newRotation.x -= Input.GetAxis("Mouse Y") * rotSpeed.x;
        newRotation.x = Mathf.Clamp(newRotation.x, pitchClamp.y, pitchClamp.x);
        rotation = Vector3.SmoothDamp(rotation, newRotation, ref dampVelocity, dampTime);

        followCamTarget.eulerAngles = rotation;

        Vector3 head = followCamTarget.position - followTarget.position;

        RaycastHit hit;

        //Between the camera and our character
        if (Physics.Raycast(followTarget.position, (followCamTarget.position - followTarget.position).normalized, out hit, maxCamDistance, notPlayerMask))
        {
            SetCamDistanceToRay(hit);
        }
        else if (Physics.Raycast(followTarget.position, followCamTarget.up, out hit, 2f))
        {
            SetCamDistanceToRay(hit);
        }
        else
            currentCamDistance = Mathf.SmoothDamp(currentCamDistance, maxCamDistance, ref collisionDampVelocity, camCollisionReturnDampTime);

        followCamTarget.position = (followTarget.position - followCamTarget.forward * currentCamDistance);
    }

    public void SetCamDistanceToRay(RaycastHit hit)
    {
        currentCamDistance = Mathf.Lerp(currentCamDistance, Vector3.Distance(hit.point, followTarget.position), camCollisionDampRate * Time.deltaTime);
        collisionDampVelocity = 0;
    }

    public void SetTarget(Transform target)
    {
        followTarget = target;
    }
}