using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Logging; 

namespace Unity.FPS.Gameplay
{
    public class HitCounter : MonoBehaviour
    {
        public static HitCounter Instance;

        [Tooltip("Text UI element to display total hits (optional)")]
        public Text hitText;

        private int hitCount = 0;
        private DataLogger logger; 

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start()
        {
            logger = FindObjectOfType<DataLogger>();
            if (logger == null)
            {
                GameObject loggerObj = new GameObject("DataLogger");
                logger = loggerObj.AddComponent<DataLogger>();
                DontDestroyOnLoad(loggerObj);
                Debug.Log("[HitCounter] DataLogger was not found in scene, so it was auto-created.");
            }
        }

        public void AddHit(GameObject shooter, GameObject target)
        {
            // Only count hits from the player
            if (shooter != null && shooter.CompareTag("Player"))
            {
                hitCount++;
                Debug.Log("Player Hits: " + hitCount);

                if (hitText != null)
                    hitText.text = "Hits: " + hitCount;

                if (logger != null)
                    logger.LogHit(hitCount); // send to DataLogger
            }
        }

        public void ResetHits()
        {
            hitCount = 0;
            if (hitText != null)
                hitText.text = "Hits: 0";

            if (logger != null)
                logger.LogHit(hitCount);
        }
    }
}