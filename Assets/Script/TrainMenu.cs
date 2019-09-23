using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainMenu : MonoBehaviour
{
    
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        HideImage();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector2(-1f, 1f);
    }

    public void TurnOnImage()
    {

    }

    public void HideImage()
    {
        image.enabled = false;
    }

    public void ShowImage()
    {
        image.enabled = true;

    }
}
