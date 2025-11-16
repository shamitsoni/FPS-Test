using UnityEngine;
using System.Threading;
using Unity.FPS.Logging;

public class JitterSimulator : MonoBehaviour
{
    [Header("Jitter Settings")]
    public int baseFrameInterval = 15;      // Minimum interval between jitters
    public int maxAdditionalInterval = 10;  // Adds 0–10 to base (so 15–25)
    public int minJitterMs = 20;            // Minimum jitter delay
    public int maxJitterMs = 100;           // Maximum jitter delay

    [Header("Dynamic Change")]
    public float intervalChangeTime = 2f;   // How often (in seconds) to randomize interval
    private float intervalTimer = 0f;

    private int frameCount = 0;
    private int currentInterval;
    private int interruptCount = 0;
    private System.Random rand = new System.Random();

    void Start()
    {
        SetRandomInterval();
    }

    void Update()
    {
        frameCount++;
        intervalTimer += Time.deltaTime;

        // Occasionally change the frame interval dynamically
        if (intervalTimer >= intervalChangeTime)
        {
            SetRandomInterval();
            intervalTimer = 0f;
        }

        // Trigger jitter every random interval
        if (frameCount % currentInterval == 0)
        {
            int jitter = rand.Next(minJitterMs, maxJitterMs);
            interruptCount++;

            // Log interrupt event to DataLogger
            if (Unity.FPS.Logging.DataLogger.Instance != null){
                Unity.FPS.Logging.DataLogger.Instance.LogInterrupt(interruptCount);
            }

            Thread.Sleep(jitter); // Simulate frame stutter
            SetRandomInterval();  // Pick a new random interval for next time
        }
    }

    void SetRandomInterval()
    {
        // Randomly choose between 15–25 frames
        currentInterval = baseFrameInterval + rand.Next(0, maxAdditionalInterval + 1);
    }
}