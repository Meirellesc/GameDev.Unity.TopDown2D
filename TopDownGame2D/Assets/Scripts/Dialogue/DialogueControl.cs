using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Serializable]
    public enum Idiom
    {
        pt_BR,
        en_US,
        es_ES
    }

    public Idiom idiom;

    [Header("Components")]
    // Dialogue window
    public GameObject dialogueObj;

    // Image of who is speaking
    public Image actorSprite;

    // Text of who is speaking
    public Text speechText;

    // Actor Name
    public Text actorNameText;

    [Header("Settings")]
    // Speed of typing text
    public float typingSpeed;

    // Control Variables
    private List<Dialogue> actorDialogues;
    public bool isDialogueVisible { get; private set; }
    private int index;
    List<string> sentences;

    public static DialogueControl instance;

    /// <summary>
    /// Awake it's called before all of Starts methods of the scripts hierarchy execution
    /// </summary>
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Calls the actor speech
    /// </summary>
    public void Speech(List<Dialogue> actorDialogues)
    {
        if(!isDialogueVisible)
        {
            dialogueObj.SetActive(true);

            this.actorDialogues = actorDialogues;

            sentences = GetSetencesInfo(this.actorDialogues);

            StartCoroutine(UpdateDialogueInfo());

            isDialogueVisible = true;
        }
    }

    /// <summary>
    /// Coroutine to showing the sentence letter by letter
    /// </summary>
    /// <returns></returns>
    IEnumerator UpdateDialogueInfo()
    {
        // Get the last sentence
        Dialogue lastDialogue = actorDialogues[index];

        actorNameText.text = lastDialogue.actorName;

        actorSprite.sprite = lastDialogue.actorSprite;

        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private List<string> GetSetencesInfo(List<Dialogue> sentences)
    {
        List<string> languages = new List<string>();

        for (int i = 0; i < sentences.Count; i++)
        {
            switch (DialogueControl.instance.idiom)
            {
                case DialogueControl.Idiom.pt_BR:
                    languages.Add(sentences[i].language.portuguese);
                    break;
                case DialogueControl.Idiom.en_US:
                    languages.Add(sentences[i].language.english);
                    break;
                case DialogueControl.Idiom.es_ES:
                    languages.Add(sentences[i].language.spanish);
                    break;
            }
        }

        return languages;
    }

    /// <summary>
    /// Jump to the next sentence
    /// </summary>
    public void NextSentence()
    {
        if(speechText.text == sentences[index])
        {
            // Check if has sentence to display
            if(index < sentences.Count - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(UpdateDialogueInfo());
            }
            else
            {
                ResetSentence();
            }
        }
    }

    /// <summary>
    /// Function to reset the sentence
    /// </summary>
    public void ResetSentence()
    {
        // Stop the Coroutine to stop fill the speech text when have to reset it
        StopAllCoroutines();

        // Reset all control variables
        speechText.text = "";
        index = 0;
        actorDialogues = null;
        isDialogueVisible = false;

        // Close dialogue HUD
        dialogueObj.SetActive(false);
    }
}
