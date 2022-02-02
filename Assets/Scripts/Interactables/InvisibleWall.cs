using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWall : MonoBehaviour
{
    [SerializeField] float starHeight = 200;
    Material m_Material;
    AudioSource tone;
    LineRenderer starLine;
    float alpha;
    Vector3 leftPoint;
    Vector3 rightPoint;

    void Start()
    {
        //Fetch the Material from the Renderer of the GameObject
        m_Material = GetComponent<Renderer>().material;
        tone = GetComponent<AudioSource>();
        alpha = 0;

        // Set alpha to 0, make transparent
        Color c = m_Material.color;
        c.a = alpha;
        m_Material.color = c;

        
    }

    private void Awake()
    {
        //Debug.Log(GetComponentsInChildren<Transform>());
        starLine = GetComponentInChildren<LineRenderer>();

        // Set Line Points up
        LineSetup();
    }


    // Update is called once per frame
    void Update()
    {
        // Draws transparent colour
        if(alpha > 0)
        {
            Color c = m_Material.color;
            c.a = alpha;
            m_Material.color = c;

            alpha = Mathf.Max(alpha - Time.deltaTime, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            alpha = 0.6f;
            tone.pitch = Random.Range(0.6f, 1.4f);
            tone.Play();
            
        }
        
    }

    // Stays lit up until player moves away
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            alpha = 0.6f;
        }
    }

    void OnDestroy()
    {
        //Destroy the instance
        Destroy(m_Material);
    }

    // Push line into sky
    void LineSetup()
    {
        // Get Positions
        Transform[] t = transform.GetComponentsInChildren<Transform>();
        leftPoint = t[2].position;
        rightPoint = t[1].position;

        //Debug.Log(starLine.positionCount);
        starLine.SetPosition(0, new Vector3(leftPoint.x, leftPoint.y + starHeight, leftPoint.z));
        starLine.SetPosition(starLine.positionCount - 1, new Vector3(rightPoint.x, rightPoint.y + starHeight, rightPoint.z));
        for (int i = 1; i < starLine.positionCount-1; i++)
        {
            // Lerp to get vector midpoints
            float tLerp = (float)i / (float)(starLine.positionCount - 1);
            Vector3 vLerp = Vector3.Lerp(leftPoint, rightPoint, tLerp);
            //Debug.Log(tLerp);
            //Debug.Log(vLerp);
            starLine.SetPosition(i, new Vector3(vLerp.x, vLerp.y + starHeight, vLerp.z));
        }
    }
}
