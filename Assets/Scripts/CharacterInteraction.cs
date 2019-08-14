using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    public float talkRange = 1;
    public float rangeMagicNumber = 50;
    private List<TalkInteraction> talkInteractions;

    /*
    void Update()
     {
         if (Input.GetButtonDown("Talk"))
         {
             var allInteractions = FindObjectsOfType<TalkInteraction>();
             Vector3 currentCharcterPosition = transform.position;
             foreach (var interaction in allInteractions)
             {               
                 //var distance = (interaction.transform.position - this.transform.position).sqrMagnitude;
                 Vector3 distanceToTalkable = interaction.transform.position - currentCharcterPosition;
                 float distanceToTarget = distanceToTalkable.magnitude;
                 Debug.Log("distance " + distanceToTarget);
                 if (distanceToTarget < talkRange)
                 {
                     interaction.Talk();
                     Debug.Log("talk");
                     break; //don't hold > once
                 }
             }
         }
     }*/

    Transform GetClosestTalkable(Transform[] talkables)
    {
        Transform bestTalkableTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in talkables)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTalkableTarget = potentialTarget;
            }
        }
        return bestTalkableTarget;
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Input.GetButtonDown("Talk"))
        {
            var allInteractions = FindObjectsOfType<TalkInteraction>();
            Vector3 currentCharcterPosition = transform.position;
            foreach (var interaction in allInteractions)
            {
                //var distance = (interaction.transform.position - this.transform.position).sqrMagnitude;
                Vector3 distanceToTalkable = interaction.transform.position - currentCharcterPosition;
                float distanceToTarget = distanceToTalkable.magnitude;
                Debug.Log("distance " + distanceToTarget);
                if (distanceToTarget < talkRange)
                {
                    interaction.Talk();
                    Debug.Log("talk");
                    break; //don't hold > once
                }
            }
        }
    }*/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, talkRange);
    }

}
