using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    public MemoryReference[] memoryReferences;

    private Section[] sections;
    private bool openedHatch = false;

    public bool locked = false;


    private void Start() {
        sections = FindObjectsOfType<Section>();
        SetCorrectSet(MemRefTracker.currentMemRef);
    }

    private void Update() {
        if ((Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.H)) && !openedHatch) {
            openedHatch = true;
            UnlockHatch();
            locked = true;
            FindObjectOfType<SubtitlesManager>().UnlockDescriptions();
        }

        if (LevelIsDone() && !openedHatch) {
            openedHatch = true;
            Invoke("UnlockHatch", 15);
            locked = true;
            FindObjectOfType<SubtitlesManager>().UnlockDescriptions();
        }
    }

    private void UnlockHatch() {
        FindObjectOfType<Hatch>().Unlock();
    }

    // USE THIS TO GO TO THE NEXT LEVEL
    public void NextMemory() {
        MemRefTracker.currentMemRef++;
        
        foreach (var s in sections) {
            s.Reset();
        }

        SetCorrectSet(MemRefTracker.currentMemRef);
        openedHatch = false;
        locked = false;
    }

    public void SetCorrectSet(int index) {
        for (int i = 0; i < memoryReferences.Length; i++) {
            memoryReferences[i].gameObject.SetActive(i == index);
        }

        foreach (var s in sections) {
            for (int i = 0; i < s.sets.Length; i++) {
                s.sets[i].correctSet = (i == memoryReferences[index].id);
            }
            
        }
    }

    private bool LevelIsDone() {
        foreach (var s in sections) {
            if (!s.done) {
                return false;
            }
        }

        return true;
    }
}
