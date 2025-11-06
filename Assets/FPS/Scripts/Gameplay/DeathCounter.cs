using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    public static DeathCounter Instance;
    public int deathCount = 0;
    public Text deathText;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddDeath()
    {
        deathCount++;
        Debug.Log("Deaths: " + deathCount);
        if (deathText != null)
            deathText.text = "Deaths: " + deathCount;
    }
}