using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour {

    public GameObject menuController;
    private FaderScript faderScript;
    private float fade;

    public AudioMixer audioMixer;
    public AudioSource audioSource;
    private float initialVolume;
    private int backgroundMusic;

    void Start() {
        faderScript = menuController.GetComponent<FaderScript>();
        LoadSettings();
    }

    private void LoadSettings() {
        initialVolume = PlayerPrefs.GetFloat("volume");
        backgroundMusic = PlayerPrefs.GetInt("backgroundMusic");
        audioMixer.SetFloat("volume", initialVolume);
        if (backgroundMusic == 1) {
            audioSource.mute = false;
        } else {
                audioSource.mute = true;
        }
    }

    IEnumerator StartForestCoroutine() {
        fade = faderScript.BeginFade(1);
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // 1 is the index of ForestScene
    }

    IEnumerator StartCityCoroutine() {
        fade = faderScript.BeginFade(1);
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(2); // 2 is the index of CityScene
    }

    public void StartForestScene() {
        StartCoroutine(StartForestCoroutine());
    }

    public void StartCityScene() {
        StartCoroutine(StartCityCoroutine());
    }

    public void ExitGame() {
        Application.Quit();
    }

}
