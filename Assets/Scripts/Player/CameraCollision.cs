using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [SerializeField] float camDist = 0.4f;  // Raycast collision radius
    [SerializeField] float camSpeed = 10f;       // How fast the camera slides back
    //[SerializeField] Transform parentTransform;     // Centre for raycasting 

    private Vector3 initPos;    // Starting local position for the camera
    private float initDist;     // initial distance from player to camera
    private int invisMask;      // Masks the invisible walls, so we cant use the camera collisions to get through the maze


    void Start()
    {
        initPos = transform.localPosition;
        initDist = (initPos - Vector3.zero).magnitude;
        invisMask = 1 << 6;     // Won't cast on invisible walls
        invisMask = ~invisMask; // inverts bits so it actually wont cast on invisible walls
    }

    // Call after the camera movement stuff is done in update
    void LateUpdate()
    {
        // Avoid changing initPos
        Vector3 currentPos = initPos;
        Vector3 parentPos = transform.parent.position;
        //Vector3 parentPos = parentTransform.position;
        RaycastHit hitInfo;
        // Get the angle the camera is at
        Vector3 direction = transform.parent.TransformPoint(initPos) - parentPos;
        //Vector3 direction = parentTransform.TransformPoint(initPos) - parentPos;
        // Cast around the camera, move back if collision occurs
        if (Physics.SphereCast(parentPos, camDist, direction, out hitInfo, initDist, invisMask))
        {
            // Move the camera back to the past to play sh
            currentPos = (initPos.normalized * (hitInfo.distance - camDist));
            currentPos.y = initPos.y;
            transform.localPosition = currentPos;
        }
        else
        {
            // Smoothly brings the camera back to original position (theoretically)
            transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, camSpeed*Time.deltaTime);
            // I don't really get lerping but hey if it works
        }
    }
}

