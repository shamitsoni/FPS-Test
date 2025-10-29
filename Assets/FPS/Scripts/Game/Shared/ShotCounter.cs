using UnityEngine;
using UnityEngine.UI;
using Unity.FPS.Logging;

namespace Unity.FPS.Game
{
    public class ShotCounter : MonoBehaviour
    {
        public static ShotCounter Instance;

        [Tooltip("Text UI element to display total shots fired (optional)")]
        public Text shotText;

        private int shotCount = 0;
        private DataLogger logger; // added

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        void Start() // added: ensure a DataLogger exists like other counters
        {
            logger = FindObjectOfType<DataLogger>();
            if (logger == null)
            {
                GameObject loggerObj = new GameObject("DataLogger");
                logger = loggerObj.AddComponent<DataLogger>();
                DontDestroyOnLoad(loggerObj);
                Debug.Log("[ShotCounter] DataLogger was not found in scene, so it was auto-created.");
            }
        }

        /// <summary>
        /// Adds a shot to the counter, but only if fired by the player.
        /// </summary>
        public void AddShot(GameObject shooter)
        {
            if (shooter == null)
                return;

            // Only count if the shooter is the Player
            if (shooter.CompareTag("Player"))
            {
                shotCount++;
                Debug.Log("Player Shots Fired: " + shotCount);

                if (shotText != null)
                    shotText.text = "Shots Fired: " + shotCount;

                if (logger != null) // send to DataLogger
                    logger.LogShot(shotCount);
            }
        }

        public void ResetShots()
        {
            shotCount = 0;
            if (shotText != null)
                shotText.text = "Shots Fired: 0";

            if (logger != null)
                logger.LogShot(shotCount);
        }
    }
}