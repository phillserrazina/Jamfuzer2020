using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCheck : MonoBehaviour
{
    public int sectionId;
    private Section mySection;
    public SubSet mySet;
    public bool isLocked { get; private set; }
    public SubSet correctSet { get; private set; }

    private Renderer myRenderer;

    public void Initialize(SubSet set) { 
        isLocked = false;
        mySection = GetComponentInParent<Section>();
        mySet = GetComponentInParent<SubSet>();
        myRenderer = GetComponent<Renderer>();
        correctSet = set;
    }

    public void TriggerLock() {
        isLocked = !isLocked;

        if (isLocked) mySection.LockObject(sectionId, this);
        else mySection.UnlockObject();

        mySet.ownsLockedObject = isLocked;
        myRenderer.material.color = isLocked ? Color.red : Color.white;
    }

    public void Switch(LockCheck other) {
        mySet.ownsLockedObject = false;

        var tempParent = other.transform.parent;
        var tempSet = other.mySet;

        other.transform.parent = transform.parent;
        other.mySet = mySet;
        other.mySet.myObjects[sectionId] = other.GetComponent<VisibilityCheck>();
        other.mySet.myLockObjects[sectionId] = other;

        transform.parent = tempParent;
        mySet = tempSet;
        mySet.myLockObjects[sectionId] = this;
        mySet.myObjects[sectionId] = GetComponent<VisibilityCheck>();
    }
}
