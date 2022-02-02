using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLeaks : MonoBehaviour
{
    [SerializeField] ParticleSystem[] waterspout;
    [SerializeField] KeyPickup[] watchedKeys;
    // Start is called before the first frame update
    void Start()
    {
        if (waterspout.Length != 6 || watchedKeys.Length != 4 )
        {
            this.enabled = false;
        }
    }

    // So this is the worst way to do things but whatever
    private void Update()
    {
        // I could have used like a for loop or function call or anything thats not this 
        if (watchedKeys[0] != null && watchedKeys[0].collected == true)
        {
            StartP1();
            watchedKeys[0] = null;
        }
        else if(watchedKeys[1] != null && watchedKeys[1].collected == true)
        {
            StartP2();
            watchedKeys[1] = null;
        }
        else if(watchedKeys[2] != null && watchedKeys[2].collected == true)
        {
            StartP3();
            watchedKeys[2] = null;
        }
        else if(watchedKeys[3] != null && watchedKeys[3].collected == true)
        {
            StartP4();
            watchedKeys[3] = null;
        }
    }

    public void StartP1()
    {
        waterspout[0].Play();
        waterspout[1].Play();
    }

    public void StartP2()
    {
        waterspout[2].Play();
    }

    public void StartP3()
    {
        waterspout[3].Play();
    }

    public void StartP4()
    {
        waterspout[4].Play();
        waterspout[5].Play();
    }
}
