using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCheck : MonoBehaviour
{
    public int sectionId;
    private Section mySection;
    private SubSet mySet;
    public bool isLocked { get; private set; }

    private void Start() { 
        isLocked = false;
        mySection = GetComponentInParent<Section>();
        mySet = GetComponentInParent<SubSet>();
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
    }

    public void Switch(LockCheck other) {
        Debug.Log("Switching " + gameObject.name + " with " + other.gameObject.name);
        var tempParent = other.transform.parent;
        var tempSet = other.mySet;

        other.transform.parent = transform.parent;
        other.mySet = mySet;

        transform.parent = tempParent;
        mySet = tempSet;
    }
}
