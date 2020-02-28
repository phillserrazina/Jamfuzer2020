using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityCheck : MonoBehaviour
{
    private Renderer myRenderer;
    public bool isVisible { get { return myRenderer.IsVisibleFrom(Camera.main); } }

    private void Start() {
        myRenderer = GetComponent<Renderer>();
    }
}
