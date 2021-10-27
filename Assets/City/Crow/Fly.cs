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
        [SerializeField] float drag = 2;

        Vector2 Force => force;
        float GravityScale => gravityScale;
        float Drag => drag;

        void FixedUpdate()
        {
            Rigidbody.gravityScale = GravityScale;
            Rigidbody.drag = Drag;
            Rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal") * Force.x, Input.GetAxis("Vertical") * Force.y));
            SpriteRenderer.flipX = Rigidbody.velocity.x < 0;

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                Crow.Land();
            }
        }
    }
}
