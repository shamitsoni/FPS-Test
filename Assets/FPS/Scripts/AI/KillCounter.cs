using UnityEngine;
using UnityEngine.UI; // needed for UI text

public class KillCounter : MonoBehaviour
{
    public static KillCounter Instance; // allows other scripts to access this one easily

    public int killCount = 0;           // the number of kills
    public Text killText;               // UI text to show kills (set this in the Inspector)

    void Awake()
    {
        // This makes sure there’s only one KillCounter in the scene
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddKill()
    {
        killCount++;
        Debug.Log("Kills: " + killCount);

        // Update UI text if it's assigned
        if (killText != null)
            killText.text = "Kills: " + killCount;
    }
}