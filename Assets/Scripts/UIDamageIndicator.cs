using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDamageIndicator : MonoBehaviour
{
    public TMP_Text label;

    void Start()
    {
        label = GetComponentInChildren<TMP_Text>();
    }

    void Update()
    {
        //Make sure the text looks at the camera - Rotate the camera every frame so it keeps looking at the target
        //rotate the transform so the forward vector points at target's current position.
        transform.LookAt(Camera.main.transform);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
