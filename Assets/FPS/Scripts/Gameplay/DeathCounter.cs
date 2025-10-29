using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.AI;

namespace Unity.FPS.Gameplay
{
    public class DeathCounter : MonoBehaviour
    {
        public static DeathCounter Instance;

        public int deathCount = 0;
        public Text deathText;
        private DataLogger logger;

        void Start()
        {
            logger = FindObjectOfType<DataLogger>();
            if (logger == null)
            {
                GameObject loggerObj = new GameObject("DataLogger");
                logger = loggerObj.AddComponent<DataLogger>();
                DontDestroyOnLoad(loggerObj);
                Debug.Log("[DeathCounter] DataLogger was not found in scene, so it was auto-created.");
            }
        }

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

            if (logger != null)
            {
                logger.LogDeath(deathCount);
            }

            if (deathText != null)
                deathText.text = "Deaths: " + deathCount;
        }
    }
}