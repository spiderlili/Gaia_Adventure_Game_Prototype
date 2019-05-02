using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool inDialogue = false;
    public DialogueManager dialogueManager;

    //TODO: Implement singleton pattern

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }
    public void TriggerDialogue()
    {
        dialogueManager.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerDialogue();
        inDialogue = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inDialogue = false;
    }

}
