using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    public static DamageCounter Instance { get; private set; }

    public float TotalDamage { get; private set; }

    void Awake()
    {
        // Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddDamage(float damage)
    {
        TotalDamage += damage;
    }

    public void ResetDamage()
    {
        TotalDamage = 0f;
    }
}