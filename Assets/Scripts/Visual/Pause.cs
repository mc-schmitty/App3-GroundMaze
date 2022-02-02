using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public bool paused;
    public bool control;
    [SerializeField] GameObject pauseCanvas;

    void Start()
    {
        paused = false;
        control = true;
        Time.timeScale = 1;
        if(pauseCanvas == null)
        {
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check when escape is pressed
        bool esc = Input.GetKeyDown(KeyCode.Escape);

        if (esc && control)
        {
            if (paused) // unpause
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                pauseCanvas.SetActive(false);
                Time.timeScale = 1;
                paused = false;
                
            }
            else    // pause
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
                paused = true;
            }
            AudioListener.pause = paused;
        }
    }
    public void Restart()
    {
        // Load Scene again
        PlayerPrefs.SetString("SceneLink", "");
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    public void MainMenu()
    {
        // Return to main menu
        GameObject.Destroy(PlayerDataTransfer.player.gameObject);
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
