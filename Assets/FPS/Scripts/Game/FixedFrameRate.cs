using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FixedFrameRate : MonoBehaviour
{
    [Range(30, 72)]
    public int targetFrameRate = 60; // Adjustable in the Editor

    // public Button button30;
    // public Button button36;
    // public Button button60;
    // public Button button72;
    public Text frameRateText;

    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        QualitySettings.vSyncCount = 0; // Ensure VSync is disabled

        // Update the text to display the initial frame rate
        UpdateFrameRateText();

        // Add listeners for button clicks
        // button30.onClick.AddListener(() => SetFrameRate(30));
        // button36.onClick.AddListener(() => SetFrameRate(36));
        // button60.onClick.AddListener(() => SetFrameRate(60));
        // button72.onClick.AddListener(() => SetFrameRate(72));
    }

    void Update()
    {
        if (Application.targetFrameRate != targetFrameRate)
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }

    void SetFrameRate(int frameRate)
    {
        targetFrameRate = frameRate;
        Application.targetFrameRate = targetFrameRate;
        UpdateFrameRateText();
    }

    void UpdateFrameRateText()
    {
        frameRateText.text = "Target Frame Rate: " + targetFrameRate.ToString();
    }
}
