using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleButtonsHover : MonoBehaviour
{

    public void PointerEnter () {
        transform. localScale = new Vector2(1.5f, 1.5f);
    }

    public void PointerExit() {
        transform. localScale = new Vector2(1f,1f);
    }

}
