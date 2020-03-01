using UnityEngine;
using System.Linq;

public class Section : MonoBehaviour
{
    // VARIABLES

    [SerializeField] private Section[] otherSections = null;
    public SubSet[] sets = null;
    private int currentSelectionInt;
    private SubSet currentlyActiveSet = null;

    public bool done = false;
    public bool inPosition = false;

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

    private ProgressionManager progressionManager;

    // EXECUTION FUNCTIONS

    private void Start() {
        progressionManager = FindObjectOfType<ProgressionManager>();
        otherSections = FindObjectsOfType<Section>().Where(s => s != this).ToArray();
        FindObjectOfType<Blink>().blinkEvent += OnBlink;

        foreach (var s in sets) {
            s.Initialize();
        }

        Scramble();

        currentSelectionInt = 2;
        currentlyActiveSet = sets[2];

        currentlyActiveSet.Load();
    }

    private void Update() {
        if (progressionManager.locked) return;

        if (isVisible) unseen = false;
        if (!isVisible && !unseen) {
            ChangeSet();
            unseen = true;
        }
    }

    // METHODS

    private void OnBlink() {
        if (progressionManager.locked) return;
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

        foreach (var s in otherSections) {
            if (s.currentlyLockedObject != null) s.currentlyLockedObject.TriggerLock();
        }

        lockedObject = id;
        currentlyLockedObject = obj;
    }

    public void UnlockObject() {
        lockedObject = -1;
        currentlyLockedObject.Switch(currentlyActiveSet.myLockObjects.Where(x => x.sectionId == currentlyLockedObject.sectionId).ToArray()[0]);
        currentlyLockedObject = null;
    }

    private void Scramble() {
        for (int i = 0; i < 20; i++) {
            int set1 = Random.Range(0, sets.Length);
            int set2 = Random.Range(0, sets.Length);

            while (set2 == set1) set2 = Random.Range(0, sets.Length);

            int index = Random.Range(0, 4);

            sets[set1].myLockObjects[index].Switch(sets[set2].myLockObjects[index]);
        }

        foreach(var s in sets) {
            s.Load();
            if (s.CheckIfAllItemsAreCorrect()) {
                Debug.Log("Section::Scramble() --- Found Insta-Done Section: Rescrambling");
                Scramble();
            }
            s.Unload();
        }
    }

    public void Reset() {
        done = false;
        Scramble();

        currentlyActiveSet.Unload();

        foreach (var s in sets) {
            s.Reset();
        }

        currentSelectionInt = 0;
        currentlyActiveSet = sets[0];

        currentlyActiveSet.Load();
    }
}
