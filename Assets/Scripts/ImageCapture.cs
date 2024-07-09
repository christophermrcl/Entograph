using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    public RawImage photoDisplayArea;
    public Camera captureCamera;
    public string saveFolder = "SavedImage";

    public GameObject shutterSFX;
    private AudioSource shutterSound;

    private FirstPersonCamera firstPersonCamera;
    private string savedName;
    private Texture2D screenCapture;

    public RawImage displayOld;
    public RawImage displayNew;
    public Canvas dupeDet;
    public Canvas newDet;
    public RawImage displaySpec;
    public TextMeshProUGUI latin;
    public TextMeshProUGUI common;

    private GameObject PauseStateObj;
    private Pause PauseGet;

    public GameObject FlashFX;
    private FlashFX FlashMethod;

    private void Start()
    {
        shutterSound = shutterSFX.GetComponent<AudioSource>();

        PauseStateObj = GameObject.FindGameObjectWithTag("Pause");
        PauseGet = PauseStateObj.GetComponent<Pause>();
           
        FlashMethod = FlashFX.GetComponent<FlashFX>();

        savedName = "";
        firstPersonCamera = GetComponent<FirstPersonCamera>();
        screenCapture = new Texture2D(Screen.width,Screen.height,TextureFormat.RGB24,false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            FlashMethod.CameraFlash();
            shutterSound.Play();
            StartCoroutine(CapturePhoto());
        }
        
    }

    IEnumerator CapturePhoto()
    {
        yield return new WaitForEndOfFrame();
        RenderTexture renderTexture = new RenderTexture(Screen.width, Screen.height, 24);
        captureCamera.targetTexture = renderTexture;
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        captureCamera.Render();
        RenderTexture.active = renderTexture;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0,false);
        screenshot.Apply();
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);
        ShowPhoto(screenshot);
        
    }


    void ShowPhoto(Texture2D screenshot)
    {
        

        Sprite photoSprite = Sprite.Create(screenshot, new Rect(0, 0, screenshot.width, screenshot.height), new Vector2(0.5f, 0.5f));

        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }

        // Convert sprite to texture
        Texture2D texture = photoSprite.texture;

        

        // Encode texture to PNG bytes
        byte[] bytes = texture.EncodeToPNG();

        GameObject nameget = GameObject.FindGameObjectWithTag("ImageName");
        ObjectDetection script = nameget.GetComponent<ObjectDetection>();
        string speciesName = script.detected;

        if(speciesName != "None")
        {
            // Define the file path
            string filePath = Path.Combine(saveFolder, speciesName + ".png");

            string fileTemp = Path.Combine(saveFolder, "temp.png");

            File.WriteAllBytes(fileTemp, bytes);

            

            if (File.Exists(filePath))
            {
                PauseGet.TurnOnPause();

                displayNew.texture = screenshot;
                byte[] fileData = File.ReadAllBytes(filePath);

                // Create a new texture and load the image data into it
                Texture2D textureOld = new Texture2D(2, 2);
                textureOld.LoadImage(fileData); // LoadImage() method automatically resizes the texture based on the image data

                // Apply the texture to a material or UI element for display
                // For example, if you have a RawImage component named "imageDisplay":
                displayOld.texture = textureOld;

                savedName = speciesName;

                dupeDet.GetComponent<Canvas>().enabled = true;

                firstPersonCamera.CursorOn();
            }
            else
            {
                newDet.enabled = true;
                latin.text = script.detected;
                common.text = script.commonname;

                displaySpec.texture = screenshot;

                PauseGet.TurnOnPause();

                File.WriteAllBytes(filePath, bytes);
            }

            Debug.Log("Sprite saved to: " + filePath);
        }

    }

    public void OldChosen()
    {
        PauseGet.TurnOffPause();
        savedName = "";
        dupeDet.GetComponent<Canvas>().enabled = false;
        firstPersonCamera.CursorOff();
    }

    public void NewChosen()
    {
        PauseGet.TurnOffPause();
        string filePath = Path.Combine(saveFolder, "temp.png");
        byte[] fileData = File.ReadAllBytes(filePath);

        // Create a new texture and load the image data into it
        Texture2D textureOld = new Texture2D(2, 2);
        textureOld.LoadImage(fileData);


        filePath = Path.Combine(saveFolder, savedName + ".png");
        byte[] bytes = textureOld.EncodeToPNG();

        File.WriteAllBytes(filePath, bytes);
        savedName = "";
        dupeDet.GetComponent<Canvas>().enabled = false;
        firstPersonCamera.CursorOff();
    }
}
