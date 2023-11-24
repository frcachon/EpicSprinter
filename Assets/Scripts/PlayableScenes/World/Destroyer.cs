using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour {

    // this script destroys the infinitely and randomly generated
    // clones of world in order to make the game more efficient
    // added later: it also destroys terrains' clones

    public string parentName;

    void Start() {
        parentName = transform.name;
        StartCoroutine(DestroyWorldClones());
        StartCoroutine(DestroyTerrainClones());
    }

    IEnumerator DestroyWorldClones() {
        yield return new WaitForSeconds(20);
        if (parentName == "World(Clone)")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyTerrainClones() {
        yield return new WaitForSeconds(60);
        if (parentName == "Terrain(Clone)")
        {
            Destroy(gameObject);
        }
    }

}
