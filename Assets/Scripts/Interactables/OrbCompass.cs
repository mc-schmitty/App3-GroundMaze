using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCompass : MonoBehaviour
{
    public float maxHeight = 25;
    bool stop;
    Transform cameraT;

    // Start is called before the first frame update
    void Start()
    {
        stop = false;
        // absolutely horriffic line of code here but hey it works
        cameraT = transform.parent.gameObject.GetComponentInChildren<Camera>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            Vector3 v = transform.position;
            float oldY = v.y;
            v = Vector3.Lerp(v, cameraT.position, Time.deltaTime);
            v.y = Mathf.Min(oldY + Time.deltaTime, maxHeight);
            
            transform.position = v;

            stop = v.y >= maxHeight;
        }
    }
}
