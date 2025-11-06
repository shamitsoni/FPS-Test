using UnityEngine;
using System.Threading;

public class JitterSimulator : MonoBehaviour {
    [Header("Jitter Settings")]
    public int frameInterval = 5;   // every N frames
    public int minJitterMs = 20;    // 20 ms
    public int maxJitterMs = 100;   // 100 ms

    private int frameCount = 0;
    private System.Random rand = new System.Random();

    void Update() {
        frameCount++;

        if (frameCount % frameInterval == 0) {
            int jitter = rand.Next(minJitterMs, maxJitterMs);
            Thread.Sleep(jitter);  // blocks main thread → visible stutter
        }
    }
}
