using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public float talkRange;
    private List<TalkInteraction> talkInteractions;
    //look for all nearby interactive objs - which is closest
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Talk"))
        {
            var allInteractions = FindObjectsOfType<TalkInteraction>();
            foreach (var interaction in allInteractions)
            {
                var distance = (interaction.transform.position - this.transform.position).magnitude;
                if(distance <= talkRange)
                {
                    interaction.Talk();
                    break; //don't hold > once
                }
            }
        }
    }

    public IEnumerator<TalkInteraction> GetEnumerator()
    {
        return talkInteractions.GetEnumerator();    
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, talkRange);
    }

}
