using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : TriggeredEvent
{
    Rigidbody rb;
    [SerializeField] GameObject collisionResult;
    [SerializeField] float triggerAfterFallingDist = 167;
    float grav = 9.9f;
    float distFell;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        distFell = 0;
    }

    public override void Activate()
    {
        triggered = true;
        
    }

    private void FixedUpdate()
    {
        if (triggered)
        {
            rb.MovePosition(rb.position + (Vector3.down * grav * Time.fixedDeltaTime));
            distFell += grav * Time.fixedDeltaTime;
            grav += grav * Time.fixedDeltaTime;

            if (distFell > triggerAfterFallingDist)
            {
                // Enable copy, disable self
                PlayerDataTransfer.player.gameObject.GetComponent<PlayerHealth>().TakeDamage(30);
                collisionResult.SetActive(true);
                transform.gameObject.SetActive(false);
            }
        }
    }
}
