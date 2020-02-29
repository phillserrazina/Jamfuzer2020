using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressionManager : MonoBehaviour
{
    public MemoryReference[] memoryReferences;

    private Section[] sections;

    private void Start() {
        sections = FindObjectsOfType<Section>();
        SetCorrectSet(MemRefTracker.currentMemRef);
    }

    private void Update() {
        if (LevelIsDone()) Debug.Log("OPEN HATCH");
        if (Input.GetKeyDown(KeyCode.Z)) {
            NextMemory();
        }
    }

    // USE THIS TO GO TO THE NEXT LEVEL
    public void NextMemory() {
        MemRefTracker.currentMemRef++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
            if (!s.done) return false;
        }

        return true;
    }
}
