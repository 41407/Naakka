using System;
using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class CrowController : MonoBehaviour
    {
        [Inject] CooldownTimer Timer { get; }

        [SerializeField] GameObject fly;
        [SerializeField] GameObject walk;

        public bool Landed { get; set; }

        bool IsFlying() => fly.activeSelf;

        public void Fly() => UnlessOnCooldown(StartFlying);

        public void Land() => UnlessOnCooldown(StartWalking);

        void UnlessOnCooldown(Action action)
        {
            if (!Timer.IsOnCooldown)
            {
                action?.Invoke();
                Timer.StartTimer(0.1f);
            }
        }

        void StartFlying()
        {
            walk.SetActive(false);
            fly.SetActive(true);
        }

        void StartWalking()
        {
            fly.SetActive(false);
            walk.SetActive(true);
        }

        public void Crash()
        {
            if (IsFlying())
            {
                Timer.Reset();
                Land();
            }
        }
    }
}
