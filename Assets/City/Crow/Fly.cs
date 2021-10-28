using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class Fly : MonoBehaviour
    {
        [Inject] CrowController Crow { get; }
        [Inject] Rigidbody2D Rigidbody { get; }

        [SerializeField] Vector2 force;
        [SerializeField] float gravityScale = 0.1f;
        [SerializeField] float maxVelocity = 5;
        [SerializeField] GameObject sharpTurnSprite;
        [SerializeField] GameObject flySprite;

        GameObject SharpTurnSprite => sharpTurnSprite;
        GameObject FlySprite => flySprite;

        Vector2 Force => force;
        float GravityScale => gravityScale;

        void FixedUpdate()
        {
            Rigidbody.gravityScale = GravityScale;
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var countersteeringCoefficient = GetCountersteeringCoefficient(input);
            Rigidbody.AddForce(new Vector2(input.x * Force.x * countersteeringCoefficient, input.y * Force.y));
            Rigidbody.AddForce(new Vector2(0, Mathf.Abs(Rigidbody.velocity.y / 5f)));

            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, maxVelocity);

            FlySprite.GetComponent<SpriteRenderer>().flipX = Rigidbody.velocity.x < 0;
            SharpTurnSprite.GetComponent<SpriteRenderer>().flipX = Rigidbody.velocity.x > 0;
            if (countersteeringCoefficient > 1)
            {
                if (FlySprite.activeSelf) FlySprite.SetActive(false);
                if (!SharpTurnSprite.activeSelf) SharpTurnSprite.SetActive(true);
            }
            else
            {
                if (SharpTurnSprite.activeSelf) SharpTurnSprite.SetActive(false);
                if (!FlySprite.activeSelf) FlySprite.SetActive(true);
            }

            if (Input.GetAxisRaw("Vertical") < 0)
            {
                Crow.Land();
            }
        }

        float GetCountersteeringCoefficient(Vector2 input)
        {
            if (Mathf.Approximately(input.x, 0f)) return 1f;
            var countersteeringFactor = 1f;
            if (SignOf(input.x) != SignOf(Rigidbody.velocity.x)) countersteeringFactor = 3f;
            return countersteeringFactor;
        }

        static int SignOf(float value) => value > 0 ? 1 : -1;
    }
}
