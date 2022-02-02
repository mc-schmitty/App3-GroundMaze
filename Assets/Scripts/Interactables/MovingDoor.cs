using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingDoor : MonoBehaviour
{
    [SerializeField] float maxheight = 2.8f;
    [SerializeField] float doorSpeed = 2;
    Transform doorT;
    float initY;
    public bool open;
    public bool close;

    // Start is called before the first frame update
    void Start()
    {   // Get the moving door component
        Transform[] lis = GetComponentsInChildren<Transform>();
        foreach(Transform t in lis)
        {
            if (t.CompareTag("Door"))
            {
                doorT = t;
                break;
            }
        }

        if(doorT == null)
        {
            // No door tag found, so door doesnt actually have a door
            GetComponent<MovingDoor>().enabled = false;
            return;
        }

        initY = doorT.position.y;

        open = false;
        close = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            doorT.position += Vector3.up * doorSpeed * Time.deltaTime;
            // Stop if door goes too far
            if (doorT.position.y > initY + maxheight)
            {
                Vector3 temp = doorT.position;
                temp.y = initY + maxheight;
                doorT.position = temp;
                open = false;
            }
        }
        else if (close)
        {
            doorT.position += Vector3.down * doorSpeed * Time.deltaTime;
            if(doorT.position.y < initY)
            {
                Vector3 temp = doorT.position;
                temp.y = initY;
                doorT.position = temp;
                close = false;
            }
        }
    }

    // Open the door
    public void Open()
    {
        open = true;
        close = false;
    }

    public void Close()
    {
        close = true;
        open = false;
    }

    // Kinda dumb thing I made to directly access the door transform
    public Transform GetDoor()
    {
        return doorT;
    }
}
