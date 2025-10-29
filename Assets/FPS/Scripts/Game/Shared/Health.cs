using UnityEngine;
using UnityEngine.Events;

namespace Unity.FPS.Game
{
    public class Health : MonoBehaviour
    {
        [Tooltip("Maximum amount of health")]
        public float MaxHealth = 10f;

        [Tooltip("Health ratio at which the critical health vignette starts appearing")]
        public float CriticalHealthRatio = 0.3f;

        public UnityAction<float, GameObject> OnDamaged;
        public UnityAction<float> OnHealed;
        public UnityAction OnDie;

        public float CurrentHealth { get; set; }
        public bool Invincible { get; set; }

        public bool CanPickup() => CurrentHealth < MaxHealth;
        public float GetRatio() => CurrentHealth / MaxHealth;
        public bool IsCritical() => GetRatio() <= CriticalHealthRatio;

        bool m_IsDead;

        void Start()
        {
            CurrentHealth = MaxHealth;
        }

        public void Heal(float healAmount)
        {
            float healthBefore = CurrentHealth;
            CurrentHealth += healAmount;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            float trueHealAmount = CurrentHealth - healthBefore;
            if (trueHealAmount > 0f)
            {
                OnHealed?.Invoke(trueHealAmount);
            }
        }

        public void TakeDamage(float damage, GameObject damageSource)
        {
            if (Invincible)
                return;

            float healthBefore = CurrentHealth;
            CurrentHealth -= damage;
            CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MaxHealth);

            // ✅ Only count damage when the PLAYER takes it
            if (CompareTag("Player") && DamageCounter.Instance != null)
            {
                DamageCounter.Instance.AddDamage(damage);
            }

            // call OnDamage action
            float trueDamageAmount = healthBefore - CurrentHealth;
            if (trueDamageAmount > 0f)
            {
                OnDamaged?.Invoke(trueDamageAmount, damageSource);
            }

            HandleDeath();
        }

        public void Kill()
        {
            // Instantly reduce to zero and trigger death
            TakeDamage(MaxHealth, null);
        }

        public void Revive()
        {
            m_IsDead = false;
            CurrentHealth = MaxHealth;
        }

        void HandleDeath()
        {
            if (m_IsDead)
                return;

            if (CurrentHealth <= 0f)
            {
                m_IsDead = true;
                OnDie?.Invoke();
            }
        }
    }
}