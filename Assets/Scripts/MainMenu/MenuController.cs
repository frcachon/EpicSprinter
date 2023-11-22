using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    public GameObject menuController;
    private FaderScript faderScript;

    private float fade;

    void Start() {
        faderScript = menuController.GetComponent<FaderScript>();
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

    public void GameSettings() {

    }

    public void ExitGame() {
        Application.Quit();
    }

}
