using QSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Manager<DialogueManager>
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public GameObject canvas;

    private Queue<Sentence> sentences; //This can also be an array if we want to go back in the list to previous dialogue options

    private GameObject AcceptButton;


    void Start()
    {
        sentences = new Queue<Sentence>();
        AcceptButton = GameObject.Find("AcceptButton");
        AcceptButton.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);
        sentences.Clear();

        foreach(Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence nextSentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(nextSentence.SentenceText));
        nameText.text = nextSentence.Name;
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);

        // TODO: Maybe replace this with a trigger and keep `EndDialogue` intact.
        if(QManager.Instance != null)
        {
            QManager.Instance.SetCurrentDiaglogueQuest();
        }
    }

    public void SetAgreeButton(bool isActive)
    {
        AcceptButton.SetActive(isActive);
    }
}
