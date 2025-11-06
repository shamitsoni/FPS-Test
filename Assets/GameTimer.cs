using UnityEngine;
using UnityEngine.UI; // ✅ Needed for Legacy UI Text

public class GameTimer : MonoBehaviour
{
    [Header("Timer Settings")]
    public float startTime = 60f; // How many seconds the timer starts with

    [Header("UI")]
    public Text timerText; // ✅ Drag your Legacy UI Text here in the Inspector

    private float timeRemaining;
    private bool timerFinished = false;

    void Start()
    {
        timeRemaining = startTime;

        // Initialize text display
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }
    }

    void Update()
    {
        if (timerFinished) return;

        // Countdown
        timeRemaining -= Time.deltaTime;

        // Update the on-screen timer text
        if (timerText != null)
        {
            timerText.text = Mathf.CeilToInt(timeRemaining).ToString();
        }

        // Check if time is up
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerFinished = true;
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        // Find the GameFlowManager in the scene
        var gameFlowManager = FindObjectOfType<Unity.FPS.Game.GameFlowManager>();
        if (gameFlowManager != null)
        {
            // Call EndGame(false) to trigger Lose sequence
            gameFlowManager.SendMessage("EndGame", false);
        }
        else
        {
            Debug.LogError("GameFlowManager not found in scene!");
        }
    }
}