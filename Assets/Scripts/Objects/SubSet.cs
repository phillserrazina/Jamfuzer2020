using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSet : MonoBehaviour
{
    public bool correctSet;
    public VisibilityCheck[] myObjects = null;
    public LockCheck[] myLockObjects = null;

    public bool ownsLockedObject;

    private void Update() {
        if (CheckIfAllItemsAreCorrect()) {
            GetComponentInParent<Section>().done = true;
            foreach (var lc in myLockObjects) {
                lc.myRenderer.material.SetColor("_OutlineColor", Color.green);
            }
            Debug.Log(transform.parent.gameObject.name + " Done");
        }
        else {
            GetComponentInParent<Section>().done = false;
        }
    }

    public void Initialize() {
        myLockObjects = new LockCheck[myObjects.Length];
        for (int i = 0; i < myObjects.Length; i++) {
            myLockObjects[i] = myObjects[i].GetComponent<LockCheck>();
            myLockObjects[i].Initialize(this);
        }
    }

    public void Load(int lockedObject=-1) {
        for (int i = 0; i < myLockObjects.Length; i++) {
            myLockObjects[i].gameObject.SetActive(i != lockedObject);
            myLockObjects[i].myRenderer.material.SetFloat("_Outline", 0f);
            myLockObjects[i].myRenderer.material.SetColor("_OutlineColor", new Color32(52, 0, 54, 255));
            if (i == lockedObject && ownsLockedObject) {
                myLockObjects[i].gameObject.SetActive(true);
                myLockObjects[i].myRenderer.material.SetFloat("_Outline", 1.1f);
                myLockObjects[i].myRenderer.material.SetColor("_OutlineColor", new Color32(173, 81, 0, 255));
            }
        }
    }

    public void Unload() {
        foreach (var obj in myLockObjects) {
            obj.gameObject.SetActive(obj.isLocked);
        }
    }

    public bool CheckIfAllItemsAreCorrect() {
        SubSet correct = myLockObjects[0].correctSet;
        foreach (var item in myLockObjects) {
            if (item.gameObject.activeSelf == false) return false;
            if (item.correctSet != correct) return false;
        }

        if (correct.correctSet) return true;
        else return false;
    }

    public void Reset() {
        ownsLockedObject = false;
        foreach (var lc in myLockObjects) {
            lc.Reset();
        }
    }
}
