using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class Walk : MonoBehaviour
    {
        [Inject] Player Player { get; }
        [Inject] CrowController Crow { get; }
        [Inject] Rigidbody2D Rigidbody { get; }
        [Inject] SpriteRenderer SpriteRenderer { get; }
        [Inject] CooldownTimer HopCooldown { get; }

        [SerializeField] Vector2 hopForce;
        [SerializeField] float gravityScale = 0.1f;

        float GravityScale => gravityScale;
        Vector2 HopForce => hopForce;
        bool CanHop => !HopCooldown.IsOnCooldown;

        void OnEnable()
        {
            HopCooldown.Reset();
            LookTowardsMovementDirection();
        }

        void LookTowardsMovementDirection() => SpriteRenderer.flipX = Rigidbody.velocity.x < 0;

        void FixedUpdate()
        {
            ApplyDownforce();
            ApplyPlayerIntention();
        }

        void ApplyPlayerIntention()
        {
            if (Crow.HasLanded && CanHop)
            {
                if (Player.WantsToHopRight())
                {
                    HopRight();
                    LookTowardsMovementDirection();
                    StartHopCooldown();
                }

                if (Player.WantsToHopLeft())
                {
                    HopLeft();
                    LookTowardsMovementDirection();
                    StartHopCooldown();
                }
            }
            else if (Crow.IsFalling)
            {
                if (PlayerIsCountersteering())
                {
                    ApplyCountersteeringForce();
                }
            }

            if (Player.WantsToFly()) Crow.Fly();
        }

        void ApplyCountersteeringForce()
        {
            Rigidbody.AddForce(new Vector2(-Mathf.Sign(Rigidbody.velocity.x) * 5f, 0));
        }

        bool PlayerIsCountersteering()
        {
            return Math.SignOf(Rigidbody.velocity.x) != Math.SignOf(Player.HorizontalInputAxis()) || Player.HorizontalInputAxis() == 0;
        }

        void ApplyDownforce()
        {
            Rigidbody.AddForce(Physics.gravity * GravityScale - Physics.gravity);
        }

        void HopLeft()
        {
            Rigidbody.AddForce(new Vector2(-1, 1) * HopForce, ForceMode2D.Impulse);
            Crow.HasLanded = false;
        }

        void HopRight()
        {
            Rigidbody.AddForce(new Vector2(1, 1) * HopForce, ForceMode2D.Impulse);
            Crow.HasLanded = false;
        }

        void StartHopCooldown()
        {
            HopCooldown.StartTimer(0.1f);
        }
    }
}
