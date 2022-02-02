using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorExit : MonoBehaviour
{
    [SerializeField] public string sceneToLoad;
    // Use to link to entrances in other scenes
    public string doorLink;

    void Start()
    {
        if (sceneToLoad == "")
            this.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPrefs.SetString("SceneLink", doorLink);
            GoToSelectedScene();
        }
    }

    private void GoToSelectedScene()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
