using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class Fly : MonoBehaviour
    {
        [Inject] CrowController Crow { get; }
        [Inject] Rigidbody2D Rigidbody { get; }
        [Inject] SpriteRenderer SpriteRenderer { get; }

        [SerializeField] Vector2 force;
        [SerializeField] float gravityScale = 0.1f;
        [SerializeField] float maxVelocity = 5;

        Vector2 Force => force;
        float GravityScale => gravityScale;

        void FixedUpdate()
        {
            Rigidbody.gravityScale = GravityScale;
            Rigidbody.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * Force.x, Input.GetAxisRaw("Vertical") * Force.y));
            Rigidbody.AddForce(new Vector2(0, Mathf.Abs(Rigidbody.velocity.y / 5f)));

            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, maxVelocity);

            SpriteRenderer.flipX = Rigidbody.velocity.x < 0;

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                Crow.Land();
            }
        }
    }
}
