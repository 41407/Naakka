using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class Walk : MonoBehaviour
    {
        [Inject] CrowController Crow { get; }
        [Inject] Rigidbody2D Rigidbody { get; }
        [Inject] SpriteRenderer SpriteRenderer { get; }
        [Inject] CooldownTimer WalkCooldown { get; }

        [SerializeField] Sprite sitSprite;
        [SerializeField] Sprite stopSprite;
        [SerializeField] Sprite diveSprite;

        [SerializeField] Vector2 hopForce;
        [SerializeField] float gravityScale = 0.1f;
        [SerializeField] float drag = 2;

        Sprite SitSprite => sitSprite;
        Sprite StopSprite => stopSprite;
        Sprite DiveSprite => diveSprite;

        float GravityScale => gravityScale;
        float Drag => drag;

        Vector2 HopForce => hopForce;

        void OnEnable()
        {
            WalkCooldown.Reset();
            SpriteRenderer.flipX = Rigidbody.velocity.x < 0;
        }

        void FixedUpdate()
        {
            Rigidbody.gravityScale = GravityScale;
            Rigidbody.drag = Drag;
            if (Crow.Landed && !WalkCooldown.IsOnCooldown)
            {
                if (Input.GetAxisRaw("Horizontal") > 0)
                {
                    Rigidbody.AddForce(new Vector2(1, 1) * HopForce, ForceMode2D.Impulse);
                    SpriteRenderer.flipX = false;
                    Crow.Landed = false;
                    WalkCooldown.StartTimer(0.1f);
                }

                if (Input.GetAxisRaw("Horizontal") < 0)
                {
                    Rigidbody.AddForce(new Vector2(-1, 1) * HopForce, ForceMode2D.Impulse);
                    SpriteRenderer.flipX = true;
                    Crow.Landed = false;
                    WalkCooldown.StartTimer(0.1f);
                }
            }

            if (Rigidbody.velocity.y < -1.9f)
            {
                if (Rigidbody.velocity.y < -2.5f)
                {
                    SpriteRenderer.sprite = DiveSprite;
                }
                else
                {
                    SpriteRenderer.sprite = StopSprite;
                }
            }
            else
            {
                SpriteRenderer.sprite = SitSprite;
            }

            if (Input.GetAxisRaw("Vertical") > 0)
            {
                Crow.Fly();
            }
        }
    }
}
