using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingWaterTimer : MonoBehaviour
{
    public float timer;
    [SerializeField] public float waterspeed;

    bool rising;

    void Start()
    {
        // Might need this for later
        rising = false;
    }

    void Update()
    {
        if (rising)
        {
            transform.position += Vector3.up * waterspeed * Time.deltaTime;
            timer += Time.deltaTime;
        }

    }

    public void StartTimer()
    {
        timer = 0;
        rising = true;
    }

    public string EndTimer()
    {
        rising = false;
        int inttime = (int)timer;
        // Return the final time as a string
        return (inttime / 60).ToString() + ":" + (inttime % 60).ToString();
    }
}
