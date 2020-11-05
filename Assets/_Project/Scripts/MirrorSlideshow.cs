﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MirrorSlideshow : MonoBehaviour
{
    public Image mirrorCanvas;
    public Text canvasText;
    public string[] filePaths;
    public float secondsToWait = 10;
    public bool isActive = true;

    public string folderName = "";
    public GameObject[] mirrorObjects;
    public Toggle mirrorToggle;

    private void Start()
    {
        canvasText.enabled = false;
        mirrorCanvas.enabled = false;
        DisplayMirror(false);
    }

    public void StartSlideshow()
    {
        if (isActive)
        {
            Debug.Log("Slideshow Has Started");
            StartCoroutine(LoadImage());
        }
    }

    IEnumerator LoadImage()
    {
        mirrorCanvas.enabled = true;
        mirrorCanvas.color = Color.white;

        filePaths = Directory.GetFiles(System.IO.Directory.GetCurrentDirectory() + @"\Configs\Stimuli\" + folderName, "*.*");

        foreach (string path in filePaths)
        {
            WWW www = new WWW("file://" + path);
            yield return www;
            Texture2D new_texture = new Texture2D(512, 512);
            www.LoadImageIntoTexture(new_texture);
            mirrorCanvas.sprite = Sprite.Create(new_texture, new Rect(0.0f, 0.0f, new_texture.width, new_texture.height), new Vector2(0.5f, 0.5f), 100.0f);
            yield return new WaitForSeconds(secondsToWait);
        }

        mirrorCanvas.color = Color.black;
        canvasText.enabled = true;
    }

    public void SetPauseLengthFromInput(string input)
    {
        float.TryParse(input.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out secondsToWait);
    }

    internal void ResetSlideshow()
    {
        StopCoroutine(LoadImage());
        canvasText.enabled = false;
        mirrorCanvas.enabled = false;
    }

    public void SetMirror(bool isActive)
    {
        this.isActive = isActive;
        mirrorToggle.isOn = isActive;
    }

    public void DisplayMirror(bool isVisible)
    {
        if (isActive && isVisible)
        {
            foreach (GameObject mirrorObject in mirrorObjects)
            {
                mirrorObject.SetActive(isVisible);
            }
        }
        else if (!isVisible)
        {
            foreach (GameObject mirrorObject in mirrorObjects)
            {
                mirrorObject.SetActive(isVisible);
            }
        }
    }
}
