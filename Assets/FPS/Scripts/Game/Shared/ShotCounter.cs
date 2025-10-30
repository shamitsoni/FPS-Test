using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class ShotCounter : MonoBehaviour
    {
        public static ShotCounter Instance;

        [Tooltip("Text UI element to display total shots fired (optional)")]
        public Text shotText;

        private int shotCount = 0;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
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
            }
        }

        public void ResetShots()
        {
            shotCount = 0;
            if (shotText != null)
                shotText.text = "Shots Fired: 0";
        }
    }
}