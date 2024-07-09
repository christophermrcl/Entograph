using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableComponent : MonoBehaviour
{
    public GameObject canvasToDisable;
    public Canvas canvas;

    public GameObject Pause;
    private Pause PauseGet;
    // Start is called before the first frame update
    void Start()
    {
        canvas = canvasToDisable.GetComponent<Canvas>();
        PauseGet = Pause.GetComponent<Pause>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canvas.enabled == true && Input.GetMouseButton(0))
        {
            canvas.enabled = false;
            PauseGet.TurnOffPause();
        }
    }
    
}
