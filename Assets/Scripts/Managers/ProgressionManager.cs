using UnityEngine;
using System.Linq;

public class ProgressionManager : MonoBehaviour
{
    public MemoryReference[] memoryReferences;

    private Section[] sections;
    private int currentMemoryReference = 0;

    private void Start() {
        sections = FindObjectsOfType<Section>();
        SetCorrectSet(currentMemoryReference);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            NextMemory();
        }
    }

    public void NextMemory() {
        Debug.Log("NEXT");
        currentMemoryReference++;
        SetCorrectSet(currentMemoryReference);
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
}
