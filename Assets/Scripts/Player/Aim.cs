using UnityEngine;
using UnityEngine.UI;

public class Aim : MonoBehaviour
{
    [SerializeField] private Text actionTextObject = null;
    private Transform selected;

    private ProgressionManager progressionManager;
    private GameManager gameManager;

    private void Start() {
        progressionManager = FindObjectOfType<ProgressionManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update() {
        if (progressionManager.locked || gameManager.paused) {
            actionTextObject.gameObject.SetActive(false);
            if (selected != null) {
                selected.GetComponent<Renderer>().material.SetFloat("_Outline", 0f);
                selected = null;
            }
            return;
        }
        
        if (selected != null) {
            if (selected.GetComponent<LockCheck>().mySet.mySection.inPosition) {
                actionTextObject.gameObject.SetActive(false);
                selected.GetComponent<Renderer>().material.SetFloat("_Outline", 0f);
                selected = null;
                return;
            }

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
            if (lc.mySet.mySection.inPosition) return;
            
            actionTextObject.text = lc.isLocked ? "Unlock" : "Lock";

            selection.GetComponent<Renderer>().material.SetFloat("_Outline", 1.1f);
            actionTextObject.gameObject.SetActive(true);
            selected = selection;
        }

        if (selected != null) {
            if (Input.GetButtonDown("Action")) {
                selected.GetComponent<LockCheck>().TriggerLock();
                FindObjectOfType<AudioManager>()?.Play("Piece");
            }
        }
    }
}
