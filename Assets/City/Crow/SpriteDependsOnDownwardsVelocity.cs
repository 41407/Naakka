using System;
using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class SpriteDependsOnDownwardsVelocity : MonoBehaviour
    {
        [Inject] Rigidbody2D Rigidbody { get; }
        [Inject] SpriteRenderer SpriteRenderer { get; }

        [SerializeField] Sprite sitSprite;
        [SerializeField] Sprite stopSprite;
        [SerializeField] Sprite diveSprite;

        [SerializeField] float stopThreshold = 2.3f;
        [SerializeField] float diveThreshold = 4.6f;

        Sprite SitSprite => sitSprite;
        Sprite StopSprite => stopSprite;
        Sprite DiveSprite => diveSprite;

        float DownwardsVelocity => -Rigidbody.velocity.y;

        void FixedUpdate()
        {
            SpriteRenderer.sprite = GetSpriteFor(DownwardsVelocity);
        }

        Sprite GetSpriteFor(float velocity)
        {
            if (velocity >= stopThreshold && velocity < diveThreshold)
            {
                return StopSprite;
            }

            if (velocity >= diveThreshold)
            {
                return DiveSprite;
            }
            else
            {
                return SitSprite;
            }
        }
    }
}
