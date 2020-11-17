using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueManager : Manager<DialogueManager>
{
    public Canvas canvas;

    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<Sentence> sentences; //This can also be an array if we want to go back in the list to previous dialogue options
    private DialogueOption[] dialogueOptions;
    private DialogueOption chosenOption;
    private GameObject AcceptButton;
    private bool dialogueOptionFinished = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentence>();
        AcceptButton = GameObject.Find("AcceptButton");
        AcceptButton.SetActive(false);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        dialogueOptionFinished = false;

        sentences.Clear();

        foreach(Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        dialogueOptions = dialogue.dialogueOptions;

        DisplayNextSentence();
    }

    public void SetAgreeButton(bool isActive)
    {
        AcceptButton.SetActive(isActive);
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            Debug.Log("No more sentences");
            if(dialogueOptions.Length == 0 || dialogueOptionFinished)
            {
                Debug.Log($"ending based on length: {dialogueOptions.Length == 0} or on optionfinished: {dialogueOptionFinished}");
                EndDialogue();
                return;
            } 
            else
            {
                //Remove continue button and only add option buttons
                transform.Find("Canvas/Dialoguebox/ContinueButton").gameObject.SetActive(false);
                for (int i = 0; i < dialogueOptions.Length; ++i)
                {
                    InstantiatieButton(dialogueOptions[i], i);
                }
                dialogueOptionFinished = true;
           }
        }
        else
        {
            Debug.Log($"Sentences remaining before dequeue: {sentences.Count}");
            Sentence nextSentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(nextSentence.SentenceText));
            nameText.text = nextSentence.Name;
        }
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
        if (QSystem.QHandler.Instance != null)
        {
            QSystem.QHandler.Instance.SetCurrentDialogueQuest();
        }
    }

    public void EnqueueDialogueAfterOption(DialogueOption option)
    {
        chosenOption = option;
        Debug.Log($"Adding dialogue after option: {option.Option}");
        foreach (Sentence sentence in option.FollowupDialogue)
        {
            sentences.Enqueue(sentence);
        }
        transform.Find("Canvas/Dialoguebox/ContinueButton").gameObject.SetActive(true);
        DisplayNextSentence();
        foreach (DialogueOption op in dialogueOptions)
        {
            Destroy(GameObject.Find(op.Option));
        }
    }

    private void InstantiatieButton(DialogueOption op, int nButton)
    {
        GameObject button = new GameObject();
        button.AddComponent<RectTransform>();
        button.GetComponent<RectTransform>().position = Vector3.zero;
        button.transform.SetParent(canvas.transform, false);
        button.AddComponent<Button>();
        button.AddComponent<Text>();
        button.name = op.Option;
        button.GetComponent<Text>().text = op.Option;
        button.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
        button.GetComponent<Text>().color = Color.black;
        button.GetComponent<RectTransform>().sizeDelta = (new Vector2(100, 20));

        button.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, -20 - nButton * 20, 0);
        button.GetComponent<Button>().onClick.AddListener(delegate { EnqueueDialogueAfterOption(op); });
    }
}
