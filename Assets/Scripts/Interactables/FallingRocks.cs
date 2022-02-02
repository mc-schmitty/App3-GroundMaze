using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRocks : TriggeredEvent
{
    Rigidbody childRock;
    float grav = 9.8f;
    float distFell;
    [SerializeField] float triggerAfterFallingDist = 9;
    [SerializeField] GameObject collisionResult;

    void Start()
    {
        childRock = GetComponentInChildren<Rigidbody>();
    }

    public override void Activate()
    {
        triggered = true;
    }

    private void FixedUpdate()
    {
        if (triggered)
        {
            childRock.MovePosition(childRock.position + (Vector3.down * grav * Time.fixedDeltaTime));
            distFell += grav * Time.fixedDeltaTime;
            grav += grav * Time.fixedDeltaTime;

            if(distFell > triggerAfterFallingDist)
            {
                // Enable copy, disable self
                collisionResult.SetActive(true);
                transform.gameObject.SetActive(false);
            }
        }
    }
}
