using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkInteraction : MonoBehaviour
{
   
    public string NodeName;

    public void Talk()
    {
        if (string.IsNullOrEmpty(NodeName) == false)
        {
            FindObjectOfType<Yarn.Unity.DialogueRunner>().StartDialogue(NodeName);
        }
    }

}
