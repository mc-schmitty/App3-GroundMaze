using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbVibrate : MonoBehaviour
{
    [SerializeField] float frequency = 0.3f;
    [SerializeField] float maxdist = 0.3f;
    Vector3 initpos;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        initpos = transform.position;
        timer = frequency + Random.Range(-frequency, frequency);    // Randomize the starting timer, create variations
    }

    // Vibrate inside update
    void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            // Make random vector
            float x = Random.Range(-maxdist, maxdist);
            float y = Random.Range(-maxdist, maxdist);
            float z = Random.Range(-maxdist, maxdist);
            Vector3 rand = Vector3.ClampMagnitude(new Vector3(x, y, z), maxdist);

            // Move the position to this vector from home
            transform.position = initpos + rand;

            // Reset timer
            timer = frequency;
        }
    }

    // Reset home location
    public void RecalcHome()
    {
        initpos = transform.position;
    }
}
