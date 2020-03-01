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

    public Renderer myRenderer { get; private set; }

    private Color32 myPurple = new Color32(52, 0, 54, 255);
    private Color32 myOrange = new Color32(173, 81, 0, 255);

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
        myRenderer.material.SetFloat("_Outline", 0f);
        myRenderer.material.SetColor("_OutlineColor", isLocked ? myOrange : myPurple);
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

    public void Reset() {
        isLocked = false;
        myRenderer.material.SetColor("_OutlineColor", myPurple);
        gameObject.SetActive(false);
    }
}
