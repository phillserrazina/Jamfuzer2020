using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{
    // VARIABLES

    [SerializeField] private GameObject[] sets = null;
    private int currentSelectionInt;
    private GameObject currentlyActiveSet = null;

    public bool debug;

    private bool unseen = true;
    private bool isVisible {
        get {
            foreach (Transform c in currentlyActiveSet.transform) {
                if (c.GetComponent<VisibilityCheck>().isVisible) return true;
            }

            return false;
        }
    }

    // EXECUTION FUNCTIONS

    private void Start() {
        FindObjectOfType<Blink>().blinkEvent += OnBlink;

        currentSelectionInt = 0;
        sets[0].SetActive(true);
        currentlyActiveSet = sets[0];
    }

    private void Update() {
        if (isVisible) unseen = false;
        if (!isVisible && !unseen) {
            ChangeSet();
            unseen = true;
            
        }
    }

    // METHODS

    private void OnBlink() {
        if (!isVisible) return;
        ChangeSet();
    }

    private void ChangeSet() {
        currentSelectionInt++;
        if (currentSelectionInt >= sets.Length) currentSelectionInt = 0;
        
        for (int i = 0; i < sets.Length; i++) {
            sets[i].SetActive(i == currentSelectionInt);
        }
    }
}
