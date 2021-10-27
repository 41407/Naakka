using UnityEngine;

namespace City.Crow
{
    public class CooldownTimer : MonoBehaviour
    {
        float cooldownTimer;
        public bool IsOnCooldown => cooldownTimer > 0;

        void Update() => UpdateCooldownTimer(Time.deltaTime);

        void UpdateCooldownTimer(float deltaTime) => cooldownTimer -= deltaTime;

        public void StartTimer(float time = 1f) => cooldownTimer = time;

        public void Reset() => cooldownTimer = -1;
    }
}
