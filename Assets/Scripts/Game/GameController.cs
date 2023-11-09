using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour {

    // historic and saved values
    private int allTimeCoins;
    private int highestScore;

    // values of each game
    private int coinsCount;
    private double distanceCount;

    // this is kind of a global state in order to know whether to execute 
    // functions or not (and optimise the game)
    public bool gameLost;
    public AudioSource coinFX;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI lostText;
    public TextMeshProUGUI recordText;
    public TextMeshProUGUI newCoinsInfoText;

    enum ExecutionModes {
        Easy,
        Hard
    };

    // this is just executed once (at the beginning), so the initial values are set here
    void Start() {
        coinsCount = 0;
        coinsText.text = "$ 0";

        distanceCount = 0;
        distanceText.text = "Distance: 0";

        gameLost = false;
        lostText.text = "";
        recordText.text = "";
        newCoinsInfoText.text = "";

        LoadData();
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
    }

    // this method restarts the scene 4 seconds after the player loses
    // it is invoked within EndOfGame()
    void Restart () {
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    // function triggered when colliding with objects tagged as PickUp
    public void SetCoinsText() {
        coinsCount++;
        coinFX.Play();
        coinsText.text = "$ " + coinsCount.ToString();
    }

    // function executed once per frame while the player doesn't lose
    public void SetDistanceText() {
        distanceCount = Math.Round(Time.timeSinceLevelLoad * 5);
        distanceText.text = "Distance: " + distanceCount.ToString();
    }

    // function executed just once (when the player collides with an obstacle)
    public void EndOfGame() {
        gameLost = true;
        SaveData();

        distanceText.text = "";
        coinsText.text = "";
        lostText.text = "You ran " + distanceCount + "m!";
        newCoinsInfoText.text = "New coins: $" + coinsCount + "\n Total coins: $" + allTimeCoins;
        
        Invoke("Restart", 4);
    }

    void Update() {
        if (!gameLost) {
            SetDistanceText();
        }
    }

}
