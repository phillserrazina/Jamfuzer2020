using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Text actionTextObject = null;
    private Transform selected;

    private void Update() {
        if (selected != null) {
            selected.GetComponent<Renderer>().material.color = selected.GetComponent<LockCheck>().isLocked ? Color.red : Color.white;
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

            selection.GetComponent<Renderer>().material.color = lc.isLocked ? Color.red : Color.yellow;
            actionTextObject.gameObject.SetActive(true);
            selected = selection;
        }

        if (selected != null) {
            if (Input.GetKeyDown(KeyCode.E)) {
                selected.GetComponent<LockCheck>().TriggerLock();
            }
        }
    }
}
