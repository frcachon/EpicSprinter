using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    // almost the script as Lab1 (currently it just rotates on the X axis)
    // the PickUps transform component is changed during the past of time
    void Update () {
        transform.Rotate (new Vector3(1, 0, 0));
    }

}
