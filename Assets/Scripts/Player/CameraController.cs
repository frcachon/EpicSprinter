using UnityEngine;
using System.Collections;
public class CameraController : MonoBehaviour {

    public GameObject player;
    private Vector3 offset;

    // This script is the same as from Lab1.
    // It's aim is that the camera follows the player while it moves.

    void Start () {
        offset = transform.position;
    }
    
    void LateUpdate () {
        transform.position = player.transform.position + offset;
    }
}