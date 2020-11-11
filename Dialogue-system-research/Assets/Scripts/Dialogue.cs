using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DialogueOption
{

    //add a question for options
    public string Option; // every option needs followup dialogue
    public Sentence[] FollowupDialogue;

    public DialogueOption(string option, Sentence[] followupDialogue)
    {
        Option = option;
        FollowupDialogue = followupDialogue;
    }
}


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


//First all the sentences will be called, after that the dialogue option will be promted with the followup sentences
[System.Serializable]
public class Dialogue
{
    public Sentence[] sentences;
    public DialogueOption[] dialogueOptions;
}
 