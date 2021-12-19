using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    public static AudioManager instance;
    [SerializeField]
    public Slider volumeSlider;

    private void Awake()
    {
        /*
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        */
    }

    void Start()
    {
        /*
        //Get the saved volume, default 0
        float volume = PlayerPrefs.GetFloat("GameVolume", 0.0f);

        //Set the audio volume to the saved volume
        AdjustAudioVolume(volume);
        */


        if(!PlayerPrefs.HasKey("GameVolume"))
        {
            PlayerPrefs.SetFloat("GameVolume", 0);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }
    /*
    private void AdjustAudioVolume(float volume)
    {
        //Update AudioMixer
        audioMixer.SetFloat("GameVolume", volume);

        //Update PlayerPrefs "GameVolume"
        PlayerPrefs.SetFloat("GameVolume", volume);

        //Save changes
        PlayerPrefs.Save();
    }
    */

    public void ChangeVolume()
    {
        audioMixer.SetFloat("GameVolume", volumeSlider.value);
        SaveVolume();
    }

    public void LoadVolume()
    {
        // volumeSlider.value = 
        PlayerPrefs.GetFloat("GameVolume");
    }

    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("GameVolume", volumeSlider.value);
    }
}
