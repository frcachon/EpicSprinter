using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteCreation : MonoBehaviour {

    public GameObject gameController;
    private GameController gc;

    // in order to optimise, generation times depend from the difficulty
    // if hard mode, player runs faster so generations are more frequent
    private int difficulty;
    private int worldSeconds;
    private int terrainSeconds;

    // variables for new worlds
    public GameObject[] worlds;
    public int zPosWorld = 50;
    public bool creatingWorld = false;
    public int secNum;

    // variables for new terrains
    public GameObject terrain;
    public int zPosTerrain = 125;
    public bool creatingTerrain = false;

    void Start() {
        gc = gameController.GetComponent<GameController>();
        difficulty = PlayerPrefs.GetInt("difficulty");
        SetTimes();
    }

    private void SetTimes() {
        if (difficulty == 0) { // easy mode
            worldSeconds = 3;
            terrainSeconds = 10;
        } else { // difficulty == 0 (hard mode)
            worldSeconds = 1;
            terrainSeconds = 5;
        }
    }

    void Update() {
        // the first if check if the game is still on, in order to optimise
        if (gc.isPlaying) {
            if (creatingWorld == false) {
                creatingWorld = true;
                StartCoroutine(GenerateWorld());
            }
            if (creatingTerrain == false) {
                creatingTerrain = true;
                StartCoroutine(GenerateTerrain());
            }
        }
    }

    IEnumerator GenerateWorld() { // every 3 seconds, randomly chooses generates a new World
        secNum = Random.Range(0, 3);
        Instantiate(worlds[secNum], new Vector3(0, 0, zPosWorld), Quaternion.identity);
        zPosWorld += 50; // this is because 50 is the lenght of every World
        yield return new WaitForSeconds(worldSeconds);
        creatingWorld = false;
    }

    IEnumerator GenerateTerrain() {
        Instantiate(terrain, new Vector3(-100, 0, zPosTerrain), Quaternion.identity);
        zPosTerrain += 150;
        yield return new WaitForSeconds(terrainSeconds);
        creatingTerrain = false;
    }

}