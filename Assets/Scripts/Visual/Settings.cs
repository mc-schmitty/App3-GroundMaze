using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public int qualityLevel = 4;
    public float volumeLevel = 1f;
    public bool isFullScreen = true;
    Slider volumeSlider;
    Slider qualitySlider;
    Text volText;
    Text qualText;
    Toggle toggle;
    AudioSource player;

    private void Start()
    {
        if(PlayerPrefs.HasKey("SavedQuality") && PlayerPrefs.HasKey("SavedVolume"))
        {
            qualityLevel = PlayerPrefs.GetInt("SavedQuality");
            volumeLevel = PlayerPrefs.GetFloat("SavedVolume");
            Debug.Log(qualityLevel + " " + volumeLevel);
        }
        else
        {
            PlayerPrefs.SetInt("SavedQuality", qualityLevel);
            PlayerPrefs.SetFloat("SavedVolume", volumeLevel);
            QualitySettings.SetQualityLevel(qualityLevel);
            AudioListener.volume = volumeLevel;
        }
    }

    void Awake()
    {
        player = GetComponentInChildren<AudioSource>();

        QualitySettings.SetQualityLevel(qualityLevel);
        AudioListener.volume = volumeLevel;

        Slider[] sList = GetComponentsInChildren<Slider>();
        volumeSlider = sList[0];
        //volumeSlider.SetValueWithoutNotify(volumeLevel);
        volumeSlider.value = volumeLevel;
        qualitySlider = sList[1];
        //qualitySlider.SetValueWithoutNotify(qualityLevel);
        qualitySlider.value = qualityLevel;

        Text[] tList = GetComponentsInChildren<Text>();
        volText = tList[1];
        volText.text = "Volume: " + (int)(volumeLevel * 100);
        qualText = tList[2];
        qualText.text = "Quality: " + QualitySettings.names[qualityLevel];

        toggle = GetComponentInChildren<Toggle>();
        toggle.isOn = Screen.fullScreen;
    }

    public void SetVolume(float number)
    {
        volumeLevel = number;
        PlayerPrefs.SetFloat("SavedVolume", volumeLevel);
        AudioListener.volume = volumeLevel;
        volText.text = "Volume: " + (int)(volumeLevel * 100);
        player.Play();
    }

    public void SetQuality(float number)
    {
        
        qualityLevel = (int)number;
        PlayerPrefs.SetInt("SavedQuality", qualityLevel);
        Debug.Log(PlayerPrefs.GetInt("SavedQuality"));
        QualitySettings.SetQualityLevel(qualityLevel);
        qualText.text = "Quality: " + QualitySettings.names[qualityLevel];
    }

    public void ToggleFullscreen(bool val)
    {
        isFullScreen = val;
        Screen.fullScreen = isFullScreen;
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }
}
