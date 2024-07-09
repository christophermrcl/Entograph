using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public int isNotPaused = 1;

    public GameObject AudioStep;
    public AudioSource step;
    void Start()
    {
        step = AudioStep.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isNotPaused == 0)
        {
            step.mute = true;

        } else
        {
            step.mute = false;
        }
    }

    public void TurnOnPause()
    {
        isNotPaused = 0;
    }

    public void TurnOffPause()
    {
        isNotPaused = 1;
    }
}
