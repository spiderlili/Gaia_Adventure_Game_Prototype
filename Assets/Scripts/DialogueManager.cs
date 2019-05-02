using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences; //FIFO to load new sentences from the end of the queue
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Animator dialogueAnimator;
    /*
    private void Awake()
    {
        int numDialogueSessions = FindObjectsOfType<DialogueManager>().Length;
        if (numDialogueSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }*/

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Start dialogue w" + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear(); //clear the queue
        dialogueAnimator.SetBool("DialogueOpen", true);
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); //current sentence
        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines(); //Stop all animations before playing the next one 
        dialogueText.text = sentence;
        StartCoroutine(TypeSentence(sentence));
    }

    //display text one by one 
    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray()) //string to char array
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        dialogueAnimator.SetBool("DialogueOpen", false);
    }

}
