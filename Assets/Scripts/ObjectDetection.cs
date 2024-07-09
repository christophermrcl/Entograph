using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ObjectDetection : MonoBehaviour
{
    public Camera mainCamera;
    public TMP_Text detection;
    public string detected;
    public string commonname;
    public string description;

    private InsectData insectData;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            
            // Check if the object has a specific tag (e.g., "YourTag")
            if (hitObject.CompareTag("Detectable"))
            {
                insectData = hitObject.GetComponent<InsectData>();
                DisplayObjectName(hitObject.name);
            }
            else {
                detection.text = "Detected Object: None";
            }
        }
    }

    void DisplayObjectName(string objName)
    {
        detection.text = "Detected Object:" + objName;
        detected = objName;
        commonname = insectData.CommonName;
    }
}
