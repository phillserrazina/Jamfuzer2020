using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibilityCheck : MonoBehaviour
{
    public int sectionId;
    private Renderer myRenderer;
    public bool isVisible { get { return myRenderer.IsVisibleFrom(Camera.main); } }
    public bool isLocked { get; private set; }

    private Section mySection;

    private void Start() {
        isLocked = false;
        myRenderer = GetComponent<Renderer>();
        mySection = GetComponentInParent<Section>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (sectionId == 1) TriggerLock();
        }
    }

    private void TriggerLock() {
        mySection.LockObject(sectionId);
        isLocked = !isLocked;
        if (!isLocked) mySection.UnlockObject();
    }
}
