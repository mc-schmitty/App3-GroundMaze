using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonScript : MonoBehaviour
{
    public float height = 0.1f;
    void Start()
    {
        // This is so dumb
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 200 * Time.deltaTime, 0));
        transform.position = new Vector3(transform.position.x, transform.position.y + height * Time.deltaTime, transform.position.z);
        //speeeen
    }
}
