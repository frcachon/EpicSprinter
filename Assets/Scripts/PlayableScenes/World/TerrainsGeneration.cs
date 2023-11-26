using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainsGeneration : MonoBehaviour {

    public GameObject gameController;
    private GameController gc;

    // in order to optimise, generation times depend from the difficulty
    // if hard mode, player runs faster so generations are more frequent
    private int difficulty;
    private int terrainSeconds;

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
            terrainSeconds = 10;
        } else { // difficulty == 0 (hard mode)
            terrainSeconds = 5;
        }
    }

    void Update() {
        // the first if check if the game is still on, in order to optimise
        if (gc.isPlaying) {
            if (creatingTerrain == false) {
                creatingTerrain = true;
                StartCoroutine(GenerateTerrain());
            }
        }
    }

    IEnumerator GenerateTerrain() {
        Instantiate(terrain, new Vector3(-100, 0, zPosTerrain), Quaternion.identity);
        zPosTerrain += 150;
        yield return new WaitForSeconds(terrainSeconds);
        creatingTerrain = false;
    }

}