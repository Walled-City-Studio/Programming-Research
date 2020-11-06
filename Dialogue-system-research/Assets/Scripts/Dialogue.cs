using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Sentence
{
    public string Name;

    [TextArea(3, 10)]
    public string SentenceText;

    public Sentence(string name, string sentencetext)
    {
        Name = name;
        SentenceText = sentencetext;
    }
}


[System.Serializable]
public class Dialogue
{
    public Sentence[] sentences;
}
