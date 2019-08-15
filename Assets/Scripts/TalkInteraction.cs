using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TalkInteraction : MonoBehaviour
{
    public string NodeName;
    [SerializeField]private DialogueRunner dialogueRunner;

    public void Talk()
    {
        if (string.IsNullOrEmpty(NodeName) == false)
        {
            if(dialogueRunner != null)
            {
                dialogueRunner.StartDialogue(NodeName);
            }
            else if (dialogueRunner == null)
            {
                FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(NodeName);
            }           
      //      Debug.Log("TALK");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    //    Debug.Log("ENTER");
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Talk();    
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Talk();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // Debug.Log("LEAVE");
    }
}

