using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempKeyPickup : MonoBehaviour
{
    ParticleSystem ambient;
    Material main;
    public float fade = 1f;
    public bool tempCollected = false;
    public bool permCollected = false;
    [SerializeField] float fadeSpeed = 0.16f;

    // Start is called before the first frame update
    void Start()
    {
        main = GetComponent<Renderer>().material;
        ambient = GetComponent<ParticleSystem>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(permCollected != true && fade < 1)
        {
            fade += fadeSpeed * Time.deltaTime;
            Color c = main.color;
            c.a = fade;
            main.color = c;
        }
        else if(fade >= 1)
        {
            tempCollected = false;
        }

        
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!permCollected && other.CompareTag("Player"))
        {
            fade = 0;
            tempCollected = true;
        }
    }

    public void PermCollect()
    {
        fade = 0;
        permCollected = true;

        // Now hide and disable this
        transform.GetComponent<MeshRenderer>().enabled = false;
        transform.GetComponent<SphereCollider>().enabled = false;
        ambient.Stop();
    }

    void OnDestroy()
    {
        //Destroy the instance
        Destroy(main);
    }
}
