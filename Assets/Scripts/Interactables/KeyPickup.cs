using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] GameObject[] linkedDoors;
    [SerializeField] public bool neg = false;       // Whether the key should give negative progress (weird)
    List<MovingDoor> doorScripts;
    ParticleSystem ambient;
    public bool collected = false;
    

    // Start is called before the first frame update
    void Start()
    {
        ambient = GetComponent<ParticleSystem>();
        doorScripts = new List<MovingDoor>();
        MovingDoor m;
        foreach(GameObject o in linkedDoors)
        {
            // So I haven't done enough research to know if this'll NOT buildup a list of the same pointer m but we'll see
            if (o.TryGetComponent<MovingDoor>(out m))
            {
                doorScripts.Add(m);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            foreach(MovingDoor d in doorScripts)
            {
                // Open all the doors
                d.Open();
            }

            // Do anything to the player here

            // Now hide and disable this
            transform.GetComponent<MeshRenderer>().enabled = false;
            transform.GetComponent<SphereCollider>().enabled = false;
            ambient.Stop();
            collected = true;
        }
    }
}
