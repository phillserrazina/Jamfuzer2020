using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool paused { get; private set; }
    private CameraEffects eye;

    [SerializeField] private GameObject pauseMenuGui = null;

    private void Awake() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        paused = false;
    }

    private void Start() {
        var am = FindObjectOfType<AudioManager>();
        eye = FindObjectOfType<CameraEffects>();
        am?.StopAllSound();
        am?.Play("Game Theme");
    }

    private void Update() {
        Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = paused;

        if (Input.GetButtonDown("Pause")) {
            Pause();
        }
    }

    public void Pause() {
        if (eye == null) eye = FindObjectOfType<CameraEffects>();
        paused = !paused;

        Time.timeScale = paused ? 0f : 1f;
        if (paused) eye.blinkAnim.SetTrigger("CloseEye");
        if (!paused) eye.blinkAnim.SetTrigger("OpenEye");

        if (!paused) pauseMenuGui.SetActive(false); 
    }

    private void TriggerPauseGui() {
        pauseMenuGui.SetActive(true);
    }

    public void QuitGame() {
        SceneManager.LoadScene(0);
    }
}
