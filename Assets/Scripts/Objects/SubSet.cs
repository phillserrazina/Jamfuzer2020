using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSet : MonoBehaviour
{
    public VisibilityCheck[] myObjects = null;
    private LockCheck[] myLockObjects = null;

    public LockCheck[] MyLockObjects { get { return myLockObjects; } }

    public bool ownsLockedObject;

    private void Start() {
        myLockObjects = new LockCheck[myObjects.Length];
        for (int i = 0; i < myObjects.Length; i++) {
            myLockObjects[i] = myObjects[i].GetComponent<LockCheck>();
        }
    }

    public void Load(int lockedObject=-1) {
        for (int i = 0; i < myLockObjects.Length; i++) {
            myLockObjects[i].gameObject.SetActive(i != lockedObject);
            if (i == lockedObject && ownsLockedObject)
                myLockObjects[i].gameObject.SetActive(true);
        }
    }

    public void Unload() {
        foreach (var obj in myLockObjects) {
            obj.gameObject.SetActive(obj.isLocked);
        }
    }
}
