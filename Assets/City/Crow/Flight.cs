using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class Flight : MonoBehaviour
    {
        [Inject] CrowController Crow { get; }
        [Inject] Player Player { get; }
        [Inject] Rigidbody2D Rigidbody { get; }

        [SerializeField] Vector2 force;
        [SerializeField] float gravityScale = 0.1f;
        [SerializeField] float maxVelocity = 5;
        [SerializeField] GameObject sharpTurnSprite;
        [SerializeField] GameObject flySprite;
        [SerializeField] float maximumHeight = 10f;

        GameObject SharpTurnSprite => sharpTurnSprite;
        GameObject FlySprite => flySprite;

        Vector2 Force => force;
        float GravityScale => gravityScale;

        void FixedUpdate()
        {
            Rigidbody.AddForce(-Physics.gravity + Physics.gravity * GravityScale);
            var input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            var countersteeringCoefficient = GetCountersteeringCoefficient(input);
            var liftFromHeight = Mathf.Clamp(maximumHeight - Rigidbody.position.y, 0, 1);
            var liftFromSpeed = Mathf.Clamp01(Mathf.Abs(Rigidbody.velocity.x / 3f));
            Rigidbody.AddForce(new Vector2(input.x * Force.x * countersteeringCoefficient, input.y * Force.y * liftFromHeight * liftFromSpeed));

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

            if (Player.WantsToLand())
            {
                Crow.Land();
            }
        }

        float GetCountersteeringCoefficient(Vector2 input)
        {
            if (Mathf.Approximately(input.x, 0f)) return 1f;
            var countersteeringFactor = 1f;
            if (Math.SignOf(input.x) != Math.SignOf(Rigidbody.velocity.x)) countersteeringFactor = 3f;
            return countersteeringFactor;
        }
    }
}
