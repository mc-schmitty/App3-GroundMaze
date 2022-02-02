using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon1Triggers : MonoBehaviour
{
    public MovingDoor DungeonDoor;
    Transform door;
    public EnemySpider Spider;
    public MainKey mainKey;
    public PlayerDataTransfer winTracker;
    //Vector3 hardCodedDoorPos = new Vector3(0.0599999987f, 3.47000003f, -24.0100002f);
    bool trigger = false;

    void Start()
    {
        // So start can run into issues if not everything is loaded
        // Moving everything to Awake fixes this - temporarily 
    }

    private void Awake()
    {
        DungeonDoor = GetComponentInChildren<MovingDoor>();
        Spider = GetComponentInChildren<EnemySpider>();
        mainKey = GetComponentInChildren<MainKey>();
        winTracker = PlayerDataTransfer.player;
        door = DungeonDoor.GetDoor();
        Spider.gameObject.SetActive(false);
    }

    private void Update()
    {
        // even putting it in awake wasn't enough i guess
        if(door == null)
        {
            door = DungeonDoor.GetDoor();
            try
            {
                Vector3 bruh = door.position;
                bruh.y += 2.8f;
                DungeonDoor.GetDoor().position = bruh;
            }
            catch (System.Exception) { }
        }

    }

    public void KillSpider()
    {
        DungeonDoor.Open();
        Spider.Die();
        winTracker.keys.Add("RedKey");
        //Debug.Log(winTracker.keys.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !trigger)
        {
            trigger = true;
            DungeonDoor.Close();
            Spider.gameObject.SetActive(true);
        }
    }
}
