using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class HitCounter : MonoBehaviour
    {
        public static HitCounter Instance;

        [Tooltip("Text UI element to display total hits (optional)")]
        public Text hitText;

        private int hitCount = 0;

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
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
            }
        }

        public void ResetHits()
        {
            hitCount = 0;
            if (hitText != null)
                hitText.text = "Hits: 0";
        }
    }
}