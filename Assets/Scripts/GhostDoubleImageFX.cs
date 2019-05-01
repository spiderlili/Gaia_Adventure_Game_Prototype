using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDoubleImageFX : MonoBehaviour
{
    public float ghostDelay = 1.0f;
    private float ghostDelaySeconds;
    [SerializeField] public GameObject ghost;

    //double image ghost effect
    [SerializeField] public GhostDoubleImageFX ghostDoubleImageFX;
    [SerializeField] public bool makeGhost = false;

    // Start is called before the first frame update
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost)
        {
            
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                //genereate a ghost
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation); //Quaternion.identiy
                Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
                currentGhost.transform.localScale = this.transform.localScale;
                currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
                ghostDelaySeconds = ghostDelay;
                Destroy(currentGhost, 1f);
            }
        }
        
    }
}
