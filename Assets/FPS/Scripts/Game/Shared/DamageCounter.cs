using UnityEngine;
using Unity.FPS.Logging;

public class DamageCounter : MonoBehaviour
{
    public static DamageCounter Instance { get; private set; }

    public float TotalDamage { get; private set; }

    private DataLogger logger;

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

    void Start()
    {
        // find or create the DataLogger (matches other counters)
        logger = FindObjectOfType<DataLogger>();
        if (logger == null)
        {
            GameObject loggerObj = new GameObject("DataLogger");
            logger = loggerObj.AddComponent<DataLogger>();
            DontDestroyOnLoad(loggerObj);
            Debug.Log("[DamageCounter] DataLogger was not found in scene, so it was auto-created.");
        }
    }

    public void AddDamage(float damage)
    {
        if (damage <= 0f)
            return;

        TotalDamage += damage;

        if (logger != null)
            logger.LogDamage(TotalDamage);
    }

    public void ResetDamage()
    {
        TotalDamage = 0f;

        if (logger != null)
            logger.LogDamage(TotalDamage);
    }
}