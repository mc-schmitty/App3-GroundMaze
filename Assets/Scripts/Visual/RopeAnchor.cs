using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeAnchor : MonoBehaviour
{
    LineRenderer rope;
    public GameObject ropeAnchor;
    public Vector3 anchorOffset;

    void Start()
    {
        rope = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // One position goes to itself, the other to the selected gameobject
        rope.SetPosition(0, transform.position);

        if (ropeAnchor.activeInHierarchy)
        {
            rope.SetPosition(1, ropeAnchor.transform.position + anchorOffset);
        }
        else
        {
            rope.SetPosition(1, transform.position);
        }
    }
}
