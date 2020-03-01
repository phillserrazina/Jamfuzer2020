using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Text actionTextObject = null;
    private Transform selected;

    private ProgressionManager progressionManager;

    private void Start() {
        progressionManager = FindObjectOfType<ProgressionManager>();
    }

    private void Update() {
        if (progressionManager.locked) {
            actionTextObject.gameObject.SetActive(false);
            if (selected != null) {
                selected.GetComponent<Renderer>().material.SetFloat("_Outline", 0f);
                selected = null;
            }
            return;
        }
        
        if (selected != null) {
            selected.GetComponent<Renderer>().material.SetFloat("_Outline", selected.GetComponent<LockCheck>().isLocked ? 1.1f : 0f);
            selected = null;
            actionTextObject.gameObject.SetActive(false);
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit)) {
            var selection = hit.transform;
            var lc = selection.GetComponent<LockCheck>();
            if (lc == null) return;
            
            actionTextObject.text = lc.isLocked ? "Unlock" : "Lock";

            selection.GetComponent<Renderer>().material.SetFloat("_Outline", 1.1f);
            actionTextObject.gameObject.SetActive(true);
            selected = selection;
        }

        if (selected != null) {
            if (Input.GetButtonDown("Action")) {
                selected.GetComponent<LockCheck>().TriggerLock();
            }
        }
    }
}
