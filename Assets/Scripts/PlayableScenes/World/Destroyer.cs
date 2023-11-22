using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    // this script destroys the infinitely and randomly generated
    // clones of world in order to make the game more efficient

    public string parentName;

    void Start() {
        parentName = transform.name;
        StartCoroutine(DestroyClone());
    }

    IEnumerator DestroyClone() {
        yield return new WaitForSeconds(20);
        if (parentName == "World(Clone)")
        {
            Destroy(gameObject);
        }
    }

}
