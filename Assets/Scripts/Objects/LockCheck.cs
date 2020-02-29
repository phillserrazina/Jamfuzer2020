using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCheck : MonoBehaviour
{
    public int sectionId;
    private Section mySection;
    private SubSet mySet;
    public bool isLocked { get; private set; }

    private Renderer myRenderer;

    public void Initialize() { 
        isLocked = false;
        mySection = GetComponentInParent<Section>();
        mySet = GetComponentInParent<SubSet>();
        myRenderer = GetComponent<Renderer>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            if (sectionId == 1) TriggerLock();
        }
    }

    private void TriggerLock() {
        mySection.LockObject(sectionId, this);
        isLocked = !isLocked;
        mySet.ownsLockedObject = isLocked;
        if (!isLocked) mySection.UnlockObject();
        myRenderer.material.color = isLocked ? Color.red : Color.white;
    }

    public void Switch(LockCheck other) {
        var tempParent = other.transform.parent;
        var tempSet = other.mySet;

        other.transform.parent = transform.parent;
        other.mySet = mySet;
        other.mySet.myLockObjects[sectionId] = other;

        transform.parent = tempParent;
        mySet = tempSet;
        mySet.myLockObjects[sectionId] = this;
    }
}
