using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWin : MonoBehaviour
{
    // So theres probably a better way to do this, but I want to sleep
    [SerializeField] GameObject water;
    [SerializeField] float waterIncreaseAmount = 0.02f;
    [SerializeField] float loseHeight = 1;
    [SerializeField] GameObject winCanvas;
    [SerializeField] GameObject loseCanvas;
    [SerializeField] GameObject HUDCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] Pause pausescript;
    [SerializeField] Text scoretext;

    Image drownImage;
    Transform waterHeight;
    RisingWaterTimer waterTimer;
    int collectedkeys;
    bool dead;
    string winscore;

    void Start()
    {
        drownImage = HUDCanvas.GetComponentInChildren<Image>();
        waterHeight = water.GetComponent<Transform>();
        waterTimer = water.GetComponent<RisingWaterTimer>();
        collectedkeys = 0;
        dead = false;
    }
    private void Update()
    {
        // Checks if water has reached players head, and updates ui
        float wh = waterHeight.position.y;
        if (!dead)
        {
            Color c = drownImage.color;
            // Changing the alpha value of the hud ring
            if (wh > loseHeight)
            {
                c.a = 0;
                drownImage.color = c;
                Lose();
                dead = true;
            }
            else
            {
                c.a = Mathf.InverseLerp(-0.1f, loseHeight + 1f, wh);
                drownImage.color = c;
            }
        }
        
            
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Key"))
        {
            // Add speed to water or remove speed if negative (weird?)
            if(other.GetComponent<KeyPickup>().neg)
                waterTimer.waterspeed -= waterIncreaseAmount;
            else
                waterTimer.waterspeed += waterIncreaseAmount;

            if (collectedkeys == 0)
                waterTimer.StartTimer();
            collectedkeys++;
        }
        else if (other.CompareTag("Win"))
        {
            Win();
        }
        else if (other.CompareTag("Step"))
        {
            // Raise the loseheight so player can stand on the stairs - for a while
            loseHeight = 4f;
        }

    }

    void Win()
    {
        // UI control
        pausescript.control = false;
        pauseCanvas.SetActive(false);
        HUDCanvas.SetActive(false);
        winCanvas.SetActive(true);
        // enable mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Score stuff
        GetComponent<PlayerMove>().enabled = false;
        string score = waterTimer.EndTimer();
        //print(score);
        scoretext.text = "Finished in: " + score;
        winscore = score;
    }

    // Used for the textinput, save name:score in data
    public void SaveWin(string name)
    {
        SaveData.AddSaveData(name, winscore);
        pausescript.MainMenu();
    }

    void Lose()
    {
        // UI control
        pausescript.control = false;
        pauseCanvas.SetActive(false);
        HUDCanvas.SetActive(false);
        loseCanvas.SetActive(true);
        // enable mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Scoring
        GetComponent<PlayerMove>().enabled = false;
        string score = waterTimer.EndTimer();
        //print(score);
    }
}
