using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHP : MonoBehaviour {
    [Range(0.0f, 1.0f)] public float fillAmount;
    public GameObject hp1;
    public GameObject hp2;
    public GameObject hp3;
    public int lives = 3;
    private float _lifeToTakeAway;

    private void Start()
    {
        _lifeToTakeAway = 1.0f / lives;
    }

    public void TakeAwayLife() {
        fillAmount -= _lifeToTakeAway;
    }

    // Update is called once per frame
    void Update () {
        if (fillAmount >= 1.0f / lives * lives) hp3.GetComponent<ShowHeartHP>().showHP = true;
        else hp3.GetComponent<ShowHeartHP>().showHP = false;

        if (fillAmount >= 1.0f / lives * (lives-2)) hp2.GetComponent<ShowHeartHP>().showHP = true;
        else hp2.GetComponent<ShowHeartHP>().showHP = false;

        if (fillAmount > 0.0f) hp1.GetComponent<ShowHeartHP>().showHP = true;
        else hp1.GetComponent<ShowHeartHP>().showHP = false;
    }
}
