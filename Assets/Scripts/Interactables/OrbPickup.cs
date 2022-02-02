using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickup : MonoBehaviour
{
    AudioSource tone;
    private void Start()
    {
        tone = GetComponent<AudioSource>();
    }
    // Just a random key pickup
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDataTransfer t = other.gameObject.GetComponent<PlayerDataTransfer>();
            t.AddKey("GreenKey");
            tone.Play();
            t.ActivateOrb();
            transform.position = transform.position + Vector3.down * 5;
        }
    }
}
