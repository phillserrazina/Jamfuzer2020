using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NaughtyAttributes;

public class SubtitlesManager : MonoBehaviour
{
    [SerializeField] Image uiImage = null;
    [SerializeField] Sprite defSpr = null;
    [SerializeField] Sprite interactSpr = null;
    [Space]
    [SerializeField] SubtitlesContainer subtitles = null;
    [SerializeField] SubtitlesContainer desciptions = null;
    [SerializeField] TextMeshProUGUI text = null;

    IEnumerator currentCoroutine;
    IEnumerator currentDescCor;
    Camera cam;

    bool descriptionsUnlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        currentCoroutine = PlaySubtitles();
        StartCoroutine(currentCoroutine);
    }

    // Update is called once per frame
    void Update()
    {
        if (descriptionsUnlocked)
        {
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                SubtitlesObjectType sub = hit.transform.GetComponent<SubtitlesObjectType>();
                if (sub != null)
                {
                    uiImage.sprite = interactSpr;
                    if (Input.GetButtonDown("Action"))
                    {
                        if (currentDescCor != null)
                        {
                            StopCoroutine(currentDescCor);
                        }
                        currentDescCor = PlayDescription((int)sub.myType);
                        StartCoroutine(currentDescCor);
                    }
                }
                else
                {
                    uiImage.sprite = defSpr;
                }
            }
        }
    }

    IEnumerator PlaySubtitles()
    {
        int randomSequenceIndex = Random.Range(0, subtitles.sequences.Length-1);
        for (int i = 0; i < subtitles.sequences[randomSequenceIndex].sentences.Length; i++)
        {
            text.color = Color.black;
            text.text = subtitles.sequences[randomSequenceIndex].sentences[i];
            yield return new WaitForSeconds(4f);
        }

        text.text = " ";
        yield return new WaitForSeconds(20f);

        currentCoroutine = PlaySubtitles();
        StartCoroutine(currentCoroutine);
    }

    IEnumerator PlayDescription(int descIndex)
    {
        StopCoroutine(currentCoroutine);
        text.color = Color.white;
        for (int i = 0; i < desciptions.sequences[descIndex].sentences.Length; i++)
        {
            text.text = desciptions.sequences[descIndex].sentences[i];
            yield return new WaitForSeconds(4f);
        }

        text.text = " ";
        yield return new WaitForSeconds(4f);
        //Restart random sentences
        StartCoroutine(currentCoroutine);
    }

    [Button]
    public void UnlockDescriptions()
    {
        descriptionsUnlocked = true;
    }

    [Button]
    public void LockDescriptions()
    {
        uiImage.sprite = defSpr;
        descriptionsUnlocked = false;
    }
}
