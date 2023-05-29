//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class PaintOnSprite : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler 
//{
//    DrawSettings drawSettings;
//    Image drawImage;
//    Sprite drawSprite;
//    Texture2D drawTexture;
//    Vector2 previousDragPosition;
//    Color[] resetColorsArray;
//    Color resetColor;
//    Color32[] currentColor;
//    RectTransform rectTransform;

//    void Awake()
//    {
//        rectTransform = GetComponent<RectTransform>();
//        drawImage = GetComponent<Image>();
//        drawSettings = new DrawSettings();
//        resetColor = new Color(0,0,0,0);

//        Initialize();
//        ResetTexture();
//    }

//    void Update()
//    {
//        KeyboardInput();
//    }

//    public void Initialize()
//    {
//        drawSprite = drawImage.sprite;
//        drawTexture = drawSprite.texture;

//        resetColorsArray = new Color[(int)drawSprite.rect.width * (int)drawSprite.rect.height];
//        for (int x = 0; x < resetColorsArray.Length; x++) resetColorsArray[x] = resetColor;
//    }


    
//}
