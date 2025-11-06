using UnityEngine;
using UnityEngine.UI;

namespace Unity.FPS.Gameplay
{
    public class HitCounterUI : MonoBehaviour
    {
        public Text hitText;

        void Start()
        {
            if (HitCounter.Instance == null)
            {
                Debug.LogError("HitCounter not found in scene!");
                return;
            }

            // Assign the UI Text reference
            HitCounter.Instance.hitText = hitText;
        }

        void Update()
        {
            // Optional live refresh (if needed)
            if (HitCounter.Instance != null && HitCounter.Instance.hitText != null)
            {
                hitText.text = HitCounter.Instance.hitText.text;
            }
        }
    }
}