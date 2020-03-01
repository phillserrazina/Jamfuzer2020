using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundManager : MonoBehaviour, ISelectHandler, IPointerEnterHandler
{
    [SerializeField] private string onSelectSound = null;
    private AudioManager audioManager;

    private void Start() {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void OnSelect(BaseEventData eventData) {
        audioManager.Play(onSelectSound, 2);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        audioManager.Play(onSelectSound, 2);
    }
}
