using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubSet : MonoBehaviour
{
    public VisibilityCheck[] myObjects = null;

    public void Load(int lockedObject=-1) {
        for (int i = 0; i < myObjects.Length; i++) {
            myObjects[i].gameObject.SetActive(i != lockedObject);
        }
    }

    public void Unload() {
        foreach (var obj in myObjects) {
            obj.gameObject.SetActive(obj.isLocked);
        }
    }
}
