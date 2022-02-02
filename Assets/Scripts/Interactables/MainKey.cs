using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKey : MonoBehaviour
{
    [SerializeField] GameObject[] linkedDoors;
    [SerializeField] float maxHeight = 3;
    List<MovingDoor> doorScripts;
    public List<TempKeyPickup> tempKeys;
    ParticleSystem ambient;
    AudioSource sound;
    public DumbParticle pickup;
    public bool collected = false;
    float maxY;
    bool rise = false;

    void Start()
    {
        maxY = transform.position.y + maxHeight;
        ambient = GetComponent<ParticleSystem>();
        sound = GetComponent<AudioSource>();
        doorScripts = new List<MovingDoor>();
        
        MovingDoor m;
        foreach (GameObject o in linkedDoors)
        {
            if (o.TryGetComponent<MovingDoor>(out m))
            {
                doorScripts.Add(m);
            }
        }
    }

    private void Update()
    {
        if (!rise)
        {
            rise = true;
            foreach (TempKeyPickup k in tempKeys)
            {
                rise = rise & k.tempCollected;
            }
            if (rise)
            {
                ambient.Play();
                foreach (TempKeyPickup k in tempKeys)
                {
                    k.PermCollect();
                }
            }
        }
        else
        {
            
            Vector3 pos = transform.position;
            // Limit Y when rising
            pos.y = Mathf.Min(pos.y + Time.deltaTime, maxY);
            transform.position = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!collected && other.CompareTag("Player"))
        {
            foreach (MovingDoor d in doorScripts)
            {
                // Open all the doors
                d.Open();
            }

            // Do anything to the player here
            sound.Play();
            pickup.Play();
            SendMessageUpwards("KillSpider");

            // Now hide and disable this
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponent<SphereCollider>().enabled = false;
            ambient.Stop();
            collected = true;
        }
    }
}
