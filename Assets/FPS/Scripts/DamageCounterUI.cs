using UnityEngine;
using TMPro; // If you use TextMeshPro
using UnityEngine.UI; // fallback if you use legacy Text

public class DamageCounterUI : MonoBehaviour
{
    public TextMeshProUGUI damageText; // Assign in Inspector if using TMP
    public Text legacyText;            // Assign if using legacy Text

    void Update()
    {
        if (DamageCounter.Instance == null)
            return;

        float total = DamageCounter.Instance.TotalDamage;

        // Update UI with whichever text component is assigned
        if (damageText != null)
            damageText.text = $"Damage Taken: {total:F0}";
        else if (legacyText != null)
            legacyText.text = $"Damage Taken: {total:F0}";
    }
}