using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataTransfer : MonoBehaviour
{
    // Whole point of this is to save a single player instance and allow it to be loaded into new scenes

    // Static var instead of weird singleton manager that i had before
    public static PlayerDataTransfer player;
    public List<string> keys;
    [SerializeField] GameObject compassOrb;

    private void Start()
    {
        if (player != null)
        {
            // There can only be one (player)
            Destroy(gameObject);
        }
        else{
            player = this;
        }
        // Makes our player indestructable (kinda)
        DontDestroyOnLoad(gameObject);
        keys = new List<string>();
        // Player will exist across scenes
        //PlayerPrefs.SetString("SceneLink", "");
    }

    public void AddKey(string key)
    {
        keys.Add(key);
        // Maybe do particles here idk
    }

    public void ActivateOrb()
    {
        compassOrb.SetActive(true);

    }
}
