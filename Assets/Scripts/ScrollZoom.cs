using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollZoom : MonoBehaviour
{
    public Camera mainCamera;
    public float zoomSpeed = 5f;
    public float minFOV = 20f;
    public float maxFOV = 60f;

    public GameObject ZoomSlider;
    private Slider Slider;

    public GameObject ZoomSFX;
    private AudioSource ZoomSound;

    private void Start()
    {

        Slider = ZoomSlider.GetComponent<Slider>();
        Slider.value = (mainCamera.fieldOfView - 20) / (maxFOV - minFOV);
    }
    void Update()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (scrollWheel != 0)
        {
            // Adjust the field of view based on the scroll wheel input
            mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView - scrollWheel * zoomSpeed, minFOV, maxFOV);
            Slider.value = (mainCamera.fieldOfView - 20) / (maxFOV - minFOV);
        }
    }
}
