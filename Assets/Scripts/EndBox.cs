using UnityEngine;
using UnityEngine.SceneManagement;

public class EndBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.GetComponent<PlayerMovement>() == null) return;

        FindObjectOfType<CameraEffects>().blinkAnim.SetTrigger("EndCloseEye");
        Invoke("GoToMenu", 3f);
    }

    private void GoToMenu() {
        SceneManager.LoadScene(0);
    }
}
