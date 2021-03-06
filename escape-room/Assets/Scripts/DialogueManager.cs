﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;

    //public Animator animator;

    private Queue<string> sentences;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
       // animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        sentences.Enqueue(">> Press the spacebar to interact with objects");
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        Button continueButton = GameObject.FindGameObjectWithTag("ContinueButton").GetComponent<Button>();
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else if (sentences.Count == 1)
        {
            nameText.text = "terminal";
        }

        string sentence = sentences.Dequeue();

        if (sentences.Count == 0)
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void addDoorFailureMessage()
    {
        sentences.Enqueue(">> The door does not open");
        sentences.Enqueue(sentences.Dequeue());
    }

    void EndDialogue()
    {
        //animator.SetBool("IsOpen", false);
    }

}
