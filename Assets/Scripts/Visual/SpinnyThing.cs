using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnyThing : MonoBehaviour
{
    [SerializeField] float spinspeed = 120f;
    [SerializeField] RisingWaterTimer waterlevel;
    [SerializeField] Pause pausescript;
    GameObject frozenplayer;


    void Start()
    {
        frozenplayer = transform.GetChild(0).gameObject;
        //Debug.Log(frozenplayer.name);
    }

    void Update()
    {
        // It spin
        transform.Rotate(0, spinspeed * Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // On a collision with a player
        if (collision.transform.CompareTag("Player"))
        {
            Transform pt = collision.transform;
            // Get the playermodel gameobject
            GameObject gc = pt.GetChild(1).gameObject;

            // Set our statue as active and take the place of the playermodel
            transform.DetachChildren();
            frozenplayer.SetActive(true);
            frozenplayer.transform.SetPositionAndRotation(gc.transform.position, gc.transform.rotation);
            OrbVibrate vb = frozenplayer.GetComponent<OrbVibrate>();
            vb.enabled = true;
            vb.RecalcHome();
            // Deactivate the old playermodel and freeze/move camera back
            //Debug.Log(gc.name);
            gc.SetActive(false);
            pt.position += pt.transform.TransformDirection(Vector3.back);
            pt.GetComponent<PlayerMove>().enabled = false;
            // And now end the game quickly
            waterlevel.waterspeed += 0.07f;
            pausescript.control = false;
        }
    }
}
