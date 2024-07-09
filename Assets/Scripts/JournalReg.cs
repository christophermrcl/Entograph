using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JournalReg : MonoBehaviour
{
    public string RegName;
    public string LatinName;
    public string EnglishName;

    public RawImage image;
    public GameObject latin;
    public GameObject english;

    private TMP_Text latinText;
    private TMP_Text englishText;

    public string saveFolder = "SavedImage";
    // Start is called before the first frame update
    void Start()
    {
        image.color = new Color(55, 55, 55, 255);
        string filePath = Path.Combine(saveFolder, RegName + ".png");
        if(File.Exists(filePath))
        {
            byte[] fileData = File.ReadAllBytes(filePath);
            // Create a new texture and load the image data into it
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(fileData); // LoadImage() method automatically resizes the texture based on the image data

            // Apply the texture to a material or UI element for display
            // For example, if you have a RawImage component named "imageDisplay":
            image.texture = texture;
            image.color = new Color(255, 255, 255, 255);

            latinText = latin.GetComponent<TMP_Text>();
            englishText = english.GetComponent<TMP_Text>();

            latinText.text = LatinName;
            englishText.text = EnglishName;
        }else
        {
            image.color = new Color(55, 55, 55, 255);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
