using UnityEngine;
using System.Threading;

public class jitter_random : MonoBehaviour {
    [Header("Jitter Settings")]
    public int minJitterMs = 20;    // 20 ms
    public int maxJitterMs = 100;   // 100 ms
    public int intervalMin = 30;    // min interval
    public int intervalMax = 40;    // max interval

    private int frameCount = 0;
    private int frameInterval;
    private float timer = 0f;
    private int interruptCount = 0;
    private System.Random rand = new System.Random();

    void Start() {
        frameInterval = rand.Next(intervalMin, intervalMax + 1);
    }

    void Update() {
        // update interval every second
        timer += Time.deltaTime;
        if (timer >= 1f) {
            timer = 0f;
            frameInterval = rand.Next(intervalMin, intervalMax + 1);
        }

        frameCount++;
        if (frameCount % frameInterval == 0) {
            interruptCount++;

            // Log interrupt event to DataLogger
            if (Unity.FPS.Logging.DataLogger.Instance != null){
                Unity.FPS.Logging.DataLogger.Instance.LogInterrupt(interruptCount);
            }

            int jitter = rand.Next(minJitterMs, maxJitterMs);
            Thread.Sleep(jitter);  // blocks main thread → visible stutter
        }
    }
}
