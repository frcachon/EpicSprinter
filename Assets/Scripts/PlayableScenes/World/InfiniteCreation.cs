using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteCreation : MonoBehaviour {

    public GameObject gameController;
    private GameController gc;

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
        yield return new WaitForSeconds(3);
        creatingWorld = false;
    }

    IEnumerator GenerateTerrain() {
        Instantiate(terrain, new Vector3(-100, 0, zPosTerrain), Quaternion.identity);
        zPosTerrain += 150;
        yield return new WaitForSeconds(10);
        creatingTerrain = false;
    }

}