using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsController : MonoBehaviour {

    public AudioMixer audioMixer;
    public AudioSource audioSource;
    public Slider volumeSlider;
    public Toggle backgroundMusicToggle;
    public TMP_Dropdown difficultyDropdown;

    private float initialVolume;
    private int backgroundMusic;
    private int difficulty;

    void Start() {
        LoadData();
    }

    private void LoadData() {
        initialVolume = PlayerPrefs.GetFloat("volume");
        backgroundMusic = PlayerPrefs.GetInt("backgroundMusic");
        difficulty = PlayerPrefs.GetInt("difficulty");

        volumeSlider.value = initialVolume;

        if (backgroundMusic == 1) {
            backgroundMusicToggle.isOn = true;
        } else {
            backgroundMusicToggle.isOn = false;
        }

        if (difficulty == 1) {
            difficultyDropdown.value = difficulty;
        } else {
            difficultyDropdown.value = 0;
        }

    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);
    }

    // 0 means backgroundMusic is muted
    public void SetBackgroundMusic(bool isPlaying) {
        audioSource.mute = !isPlaying;
        if (isPlaying) {
            PlayerPrefs.SetInt("backgroundMusic", 1);
        } else {
            PlayerPrefs.SetInt("backgroundMusic", 0);
        }
    }

    // difficulty == 0 means "easy", difficulty == 1 means "hard"
    public void SetDifficulty(int index) {
        if (index == 1) {
            PlayerPrefs.SetInt("difficulty", 1);
        } else {
            PlayerPrefs.SetInt("difficulty", 0);
        }
    }

}
