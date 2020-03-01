using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "New SubtitlesContainer", menuName = "Subtitles Container")]
[System.Serializable]
public class SubtitlesContainer : ScriptableObject
{
    //public string[] senteces;
    public SentenceSequence[] sequences;
}

[System.Serializable]
public class SentenceSequence
{
    public string[] sentences;
}
