using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDungeonRubble : MonoBehaviour
{
    [SerializeField] MovingDoor door;
    [SerializeField] List<TempKeyPickup> tKeys;
    [SerializeField] GameObject normalEntrance;
    [SerializeField] GameObject brokenEntrance;
    PlayerDataTransfer winCon;

    private void Awake()
    {
        winCon = PlayerDataTransfer.player;
        try
        {
            if (winCon.keys.Contains("RedKey"))
            {
                normalEntrance.SetActive(false);
                brokenEntrance.SetActive(true);
                this.enabled = false;
            }
        }
        catch(System.Exception) { } // First time in room, so data doesn't matter
    }

    // Update is called once per frame
    void Update()
    {
        bool c = true;
        foreach(TempKeyPickup kp in tKeys)
        {
            c = c & kp.tempCollected;
        }

        if (c)
        {
            foreach(TempKeyPickup kp in tKeys)
            {
                kp.PermCollect();
            }
            door.Open();
            this.enabled = false;
        }
    }
}
