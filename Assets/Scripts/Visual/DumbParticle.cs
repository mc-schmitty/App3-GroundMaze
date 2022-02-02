using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbParticle : MonoBehaviour
{
    ParticleSystem pplay;

    void Start()
    {
        pplay = GetComponent<ParticleSystem>();
    }

    public void Play()
    {
        pplay.Play();
    }

    public void Stop()
    {
        pplay.Stop();
    }
}
