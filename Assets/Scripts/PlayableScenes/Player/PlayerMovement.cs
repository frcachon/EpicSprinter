using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float runningSpeed;
    public float horizontalSpeed;
    public GameObject gameController;

    public bool isJumping = false;
    public bool comingDown = false;
    public GameObject player;

    private GameController gc;
    private float smoothness;
    private float rotationAngle;
    private int difficulty;

    public AudioSource jumpFX;

    void Start() {
        gc = gameController.GetComponent<GameController>();
        smoothness = 22.0f;
        rotationAngle = 30.0f;
        difficulty = PlayerPrefs.GetInt("difficulty");
        SetSpeed();
    }

    private void SetSpeed() {
        if (difficulty == 0) { // easy mode
            runningSpeed = 12.0f;
            horizontalSpeed = 9.0f;
        } else { // difficulty == 0 (hard mode)
            runningSpeed = 20.0f;
            horizontalSpeed = 13.0f;
        }
    }

    // player collision with an object tagged as "PickUp"
    void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "PickUp") {
            other.gameObject.SetActive(false);
            gc.SetCoinsText();
        }
    }

    // player collision with an object tagged as "Obstacle"
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Obstacle") {
            runningSpeed = 0;
            horizontalSpeed = 0;
            gc.EndOfGame();
            player.GetComponent<Animator>().Play("Stumble Backwards");
        }
    }

    void FixedUpdate() {
        // continuous movement in the forward direction
        transform.Translate(Vector3.forward * Time.deltaTime * runningSpeed, Space.World);

        // whenever the character moves horizontally, it rotates to be more realistic
        float horizontalInput = Input.GetAxis("Horizontal");
        if (gc.isPlaying) {
            Quaternion quat = Quaternion.Euler(0, horizontalInput * rotationAngle, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, quat,  Time.deltaTime * smoothness);
        }

        // horizontal movement associated to the arrows and movement boundary
        if (horizontalInput > 0 && horizontalInput <= 1) { // Right key
            if (transform.position.x < 4.5f) { // movement constraint
                transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
            }
        }
        if (horizontalInput < 0 && horizontalInput >= -1) { // Left key
            if (transform.position.x > -4.5f) { // movement constraint
                transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
            }
        }

        // jumping
        if (Input.GetKey(KeyCode.UpArrow)) {
            if (gc.isPlaying) {
                jumpFX.Play();
                if (!isJumping) {
                    isJumping = true;
                    StartCoroutine(JumpSequence());
                }
            }
        }

        if (isJumping) {
            if (!comingDown) {
                transform.Translate(Vector3.up * Time.deltaTime * 10);
            }
            if (comingDown) {
                transform.Translate(Vector3.up * Time.deltaTime * -10);
            }
        }
    }

    IEnumerator JumpSequence() {
        yield return new WaitForSeconds(0.4f);
        comingDown = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
        comingDown = false;
        if (gc.isPlaying) {
        player.GetComponent<Animator>().Play("Goofy Running");
        }
    }

}
