using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    public string doorLink;

    void Start()
    {
        if(PlayerPrefs.GetString("SceneLink") == doorLink && PlayerDataTransfer.player != null)
        {
            // Teleport Player to this position
            //Debug.Log(PlayerDataTransfer.player.transform.eulerAngles + " " + transform.eulerAngles);
            PlayerDataTransfer.player.transform.position = new Vector3(transform.position.x, transform.position.y+1, transform.position.z);
            PlayerDataTransfer.player.GetComponent<PlayerMove>().SetRotation(transform.eulerAngles);
            //Debug.Log(PlayerDataTransfer.player.transform.eulerAngles);
        }

        // Disable object, doesn't render in game
        gameObject.SetActive(false);
    }

}
