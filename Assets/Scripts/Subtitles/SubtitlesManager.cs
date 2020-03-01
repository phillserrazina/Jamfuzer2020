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
    [SerializeField] TextMeshProUGUI text;

    IEnumerator currentCoroutine;
    Camera cam;
    [ReadOnly] List<int> playedDescriptions = new List<int>();
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
                    if (!playedDescriptions.Contains((int)sub.myType))
                    {
                        playedDescriptions.Add((int)sub.myType);
                        StartCoroutine(PlayDescription((int)sub.myType));
                    }
                }
            }
            else
            {
                uiImage.sprite = defSpr;
            }
        }
    }

    IEnumerator PlaySubtitles()
    {
        int randomSequenceIndex = Random.Range(0, subtitles.sequences.Length-1);
        for (int i = 0; i < subtitles.sequences[randomSequenceIndex].sentences.Length; i++)
        {
            Debug.Log(randomSequenceIndex);
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
}
