using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    // VARIABLES

    [SerializeField] private Color[] myColors = null;
    private int currentSelectionInt;
    
    private Renderer myRenderer;

    public bool debug;

    private bool unseen = true;
    private bool isVisible { get { return myRenderer.IsVisibleFrom(Camera.main); } }

    // EXECUTION FUNCTIONS

    private void Start() {
        myRenderer = GetComponent<Renderer>();
        FindObjectOfType<Blink>().blinkEvent += OnBlink;

        currentSelectionInt = 0;
        myRenderer.material.color = myColors[0];
    }

    private void Update() {
        if (isVisible) unseen = false;
        if (!isVisible && !unseen) {
            ChangeColor();
            unseen = true;
            
        }
    }

    private void OnBecameInvisible() {
        ChangeColor();
    }

    // METHODS

    private void OnBlink() {
        if (!isVisible) return;
        ChangeColor();
    }

    private void ChangeColor() {
        currentSelectionInt++;
        if (currentSelectionInt >= myColors.Length) currentSelectionInt = 0;
        myRenderer.material.color = myColors[currentSelectionInt];
    }
}
