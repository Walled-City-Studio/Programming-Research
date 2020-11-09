using QSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
public class DialogueManager : Manager<DialogueManager>
=======
public class DialogueManager : MonoBehaviour
>>>>>>> parent of 9c55c73... Latest
=======
public class DialogueManager : MonoBehaviour
>>>>>>> parent of 9c55c73... Latest
=======
public class DialogueManager : MonoBehaviour
>>>>>>> parent of 9c55c73... Latest
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<Sentence> sentences; //This can also be an array if we want to go back in the list to previous dialogue options

    private Quest CurrentQuest;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();
    }

    public void SetCurrentQuest(Quest quest)
<<<<<<< HEAD
<<<<<<< HEAD
    {
        CurrentQuest = quest;
    }

    public void StartDialogue(Dialogue dialogue, Quest quest = null)
    {
=======
=======
>>>>>>> parent of 9c55c73... Latest
    {
        CurrentQuest = quest;
    }

    public void StartDialogue(Dialogue dialogue, Quest quest = null)
    {
<<<<<<< HEAD
>>>>>>> parent of 9c55c73... Latest
=======
>>>>>>> parent of 9c55c73... Latest
        if (quest != null)
        {
            SetCurrentQuest(quest);
        }
          
        animator.SetBool("isOpen", true);

        sentences.Clear();

        foreach(Sentence sentence in dialogue.sentences)
        {
            Debug.Log("asdasd111");
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
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

        // TODO: Maybe replace this with a trigger and keep `EndDialogue` intact.
        if(QManager.Instance != null)
        {
            QManager.Instance.SetCurrentDiaglogueQuest();
        }
=======
>>>>>>> parent of 9c55c73... Latest
=======
>>>>>>> parent of 9c55c73... Latest
=======
>>>>>>> parent of 9c55c73... Latest
    }

}
