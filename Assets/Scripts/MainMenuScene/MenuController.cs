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

    IEnumerator ChangeLevel() {
        fade = faderScript.BeginFade(1);
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(1); // 1 is the index of MainScene

    }

    public void StartGame() {
        StartCoroutine(ChangeLevel());
    }

    public void GameSettings() {

    }

    public void ExitGame() {
        Application.Quit();
    }

}
