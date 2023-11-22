using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteCreation : MonoBehaviour {

    public GameObject[] worlds;
    public int zPos = 50;
    public bool creatingSection = false;
    public int secNum;
    public GameObject gameController;

    private GameController gc;

    void Start() {
        gc = gameController.GetComponent<GameController>();
    }

    void Update() {
        // the first if check if the game is still on, in order to optimise
        if (!gc.gameLost) {
            if (creatingSection == false) {
                creatingSection = true;
                StartCoroutine(GenerateSection());
            }
        }
    }

    IEnumerator GenerateSection() { // every 3 seconds, randomly chooses generates a new World
        secNum = Random.Range(0, 3);
        Instantiate(worlds[secNum], new Vector3(0, 0, zPos), Quaternion.identity);
        zPos += 50; // this is because 50 is the lenght of every World
        yield return new WaitForSeconds(3);
        creatingSection = false;
    }

}