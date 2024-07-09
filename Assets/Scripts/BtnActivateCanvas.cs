using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BtnActivateCanvas : MonoBehaviour
{
    public GameObject windowToActive;

    public void ActivateWindow()
    {
        windowToActive.SetActive(true);
    }
    public void DeactivateWindow()
    {
        windowToActive.SetActive(false);
    }
}
