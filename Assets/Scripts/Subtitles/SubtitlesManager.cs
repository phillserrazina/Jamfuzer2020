using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SubtitlesManager : MonoBehaviour
{
    [SerializeField] SubtitlesContainer subtitles = null;
    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlaySubtitles());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator PlaySubtitles()
    {
        int randomSequenceIndex = Random.Range(0, subtitles.sequences.Length-1);
        Debug.Log(randomSequenceIndex);
        for (int i = 0; i < subtitles.sequences[randomSequenceIndex].sentences.Length; i++)
        {
            text.text = subtitles.sequences[randomSequenceIndex].sentences[i];
            yield return new WaitForSeconds(4f);
        }

        text.text = " ";
        yield return new WaitForSeconds(15f);

        StartCoroutine(PlaySubtitles());
    }
}
