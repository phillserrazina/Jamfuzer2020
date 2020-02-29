using UnityEngine;
using System.Linq;

public class Section : MonoBehaviour
{
    // VARIABLES

    [SerializeField] private SubSet[] sets = null;
    private int currentSelectionInt;
    private SubSet currentlyActiveSet = null;

    public bool debug;

    private bool unseen = true;
    private bool isVisible {
        get {
            foreach (VisibilityCheck c in currentlyActiveSet.myObjects) {
                if (c.isVisible) return true;
            }

            return false;
        }
    }

    private int lockedObject = -1;
    public LockCheck currentlyLockedObject = null;

    // EXECUTION FUNCTIONS

    private void Start() {
        FindObjectOfType<Blink>().blinkEvent += OnBlink;

        currentSelectionInt = 0;
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
        currentlyActiveSet = sets[currentSelectionInt];
        
        for (int i = 0; i < sets.Length; i++) {
            if (i == currentSelectionInt) sets[i].Load(lockedObject);
            else sets[i].Unload();
        }
    }

    public void LockObject(int id, LockCheck obj) {
        if (currentlyLockedObject != null) currentlyLockedObject.TriggerLock();

        lockedObject = id;
        currentlyLockedObject = obj;
    }

    public void UnlockObject() {
        lockedObject = -1;
        currentlyLockedObject.Switch(currentlyActiveSet.myLockObjects.Where(x => x.sectionId == currentlyLockedObject.sectionId).ToArray()[0]);
        currentlyLockedObject = null;
    }
}
