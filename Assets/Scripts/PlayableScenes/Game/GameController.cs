using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;

public class GameController : MonoBehaviour {

    // player controller in order to know velocities (and calculate distance)
    public GameObject playerMovement;
    private PlayerMovement pm;

    // historic and saved values
    private int allTimeCoins;
    private int highestScore;
    private float initialVolume;
    private int backgroundMusic;

    // GameObjects related to volume and music
    public AudioMixer audioMixer;
    public AudioSource audioSource;

    // values of each game
    private int coinsCount;
    private double distanceCount;

    // this is kind of a global state in order to know whether to execute 
    // functions or not (and optimise the game)
    public bool isPlaying;
    public AudioSource coinFX;
    public AudioSource gameOverFX;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI lostText;
    public TextMeshProUGUI recordText;
    public TextMeshProUGUI newCoinsInfoText;

    public GameObject gameController;
    private FaderScript faderScript;
    private float fade;

    // this is just executed once (at the beginning), so the initial values are set here
    void Start() {
        faderScript = gameController.GetComponent<FaderScript>();
        pm = playerMovement.GetComponent<PlayerMovement>();

        coinsCount = 0;
        coinsText.text = "$ 0";

        distanceCount = 0;
        distanceText.text = "Distance: 0";

        isPlaying = true;
        lostText.text = "";
        recordText.text = "";
        newCoinsInfoText.text = "";

        LoadData();

    }

    // coroutine provided for Lab3, adapted for the game
    IEnumerator ChangeLevel() {
        yield return new WaitForSeconds(4);
        fade = faderScript.BeginFade(1);
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0); // 0 is the index of MainMenuScene
    }

    //as the saved data is not sensitive, it can be saved using PlayerPrefs (it is not encrypted)
    void SaveData() {
        // at the end of every game, the allTimeCoins count updates itself and is displayed below the score
        PlayerPrefs.SetInt("AllTimeCoins", PlayerPrefs.GetInt("AllTimeCoins") + coinsCount);
        allTimeCoins = PlayerPrefs.GetInt("AllTimeCoins");

        // also, if the distance is higher than the current record, a text will tell it to the player
        // and the new value will be saved
        if ((int) distanceCount > highestScore) {
            PlayerPrefs.SetInt("HighestScore", (int) distanceCount);
            highestScore = PlayerPrefs.GetInt("HighestScore");
            recordText.text = "NEW RECORD!";
        }
    }

    void LoadData() {
        allTimeCoins = PlayerPrefs.GetInt("AllTimeCoins");
        highestScore = PlayerPrefs.GetInt("HighestScore");
        initialVolume = PlayerPrefs.GetFloat("volume");
        backgroundMusic = PlayerPrefs.GetInt("backgroundMusic");

        audioMixer.SetFloat("volume", initialVolume);

        if (backgroundMusic == 1) {
            audioSource.mute = false;
        } else {
                audioSource.mute = true;
        }
    }

    // function triggered when colliding with objects tagged as PickUp
    public void SetCoinsText() {
        coinsCount++;
        coinFX.Play();
        coinsText.text = "$ " + coinsCount.ToString();
    }

    // function executed once per frame while the player doesn't lose
    public void SetDistanceText() {
        distanceCount = Math.Round(Time.timeSinceLevelLoad * pm.horizontalSpeed/2);
        distanceText.text = "Distance: " + distanceCount.ToString();
    }

    // function executed just once (when the player collides with an obstacle)
    public void EndOfGame() {
        isPlaying = false;
        gameOverFX.Play();
        SaveData();
        distanceText.text = "";
        coinsText.text = "";
        lostText.text = "You ran " + distanceCount + "m!";
        newCoinsInfoText.text = "New coins: $" + coinsCount + "\n Total coins: $" + allTimeCoins;
        
        StartCoroutine(ChangeLevel());
    }

    void Update() {
        if (isPlaying) {
            SetDistanceText();
        }
    }

}
