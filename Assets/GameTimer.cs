using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public float startTime = 60f;
    private float timeRemaining;

    private bool timerFinished = false;

    void Start()
    {
        timeRemaining = startTime;
    }

    void Update()
    {
        if (timerFinished) return;

        timeRemaining -= Time.deltaTime;
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