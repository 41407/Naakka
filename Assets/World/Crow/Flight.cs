using UnityEngine;
using Zenject;

namespace World.Crow
{
    public class Flight : MonoBehaviour
    {
        [Inject] CrowController Crow { get; }
        [Inject] Player Player { get; }
        [Inject] Rigidbody2D Rigidbody { get; }

        [SerializeField] Vector2 force;
        [SerializeField] float gravityScale = 0.1f;
        [SerializeField] float maxVelocity = 5;
        [SerializeField] GameObject sharpTurnView;
        [SerializeField] GameObject flyView;
        [SerializeField] float maximumHeight = 10f;

        GameObject SharpTurnView => sharpTurnView;
        GameObject FlyView => flyView;

        Vector2 Force => force;
        float GravityScale => gravityScale;

        void FixedUpdate()
        {
            Rigidbody.AddForce(-Physics.gravity + Physics.gravity * GravityScale);
            var input = Player.GetInput();
            var countersteeringCoefficient = GetCountersteeringCoefficient(input);
            var liftFromHeight = GetLiftByHeight();
            var liftFromSpeed = GetLiftFromSpeed();

            Rigidbody.AddForce(new Vector2(input.x * Force.x * countersteeringCoefficient, input.y * Force.y * liftFromHeight * liftFromSpeed));

            LimitVelocity();

            UpdateSprite(countersteeringCoefficient);

            if (Player.WantsToLand()) Crow.Land();
        }

        void UpdateSprite(float countersteeringCoefficient)
        {
            FlyView.GetComponent<SpriteRenderer>().flipX = Rigidbody.velocity.x < 0;
            SharpTurnView.GetComponent<SpriteRenderer>().flipX = Rigidbody.velocity.x > 0;
            if (countersteeringCoefficient > 1)
            {
                if (FlyView.activeSelf) FlyView.SetActive(false);
                if (!SharpTurnView.activeSelf) SharpTurnView.SetActive(true);
            }
            else
            {
                if (SharpTurnView.activeSelf) SharpTurnView.SetActive(false);
                if (!FlyView.activeSelf) FlyView.SetActive(true);
            }
        }

        void LimitVelocity()
        {
            Rigidbody.velocity = Vector2.ClampMagnitude(Rigidbody.velocity, maxVelocity);
        }

        float GetLiftFromSpeed()
        {
            return Mathf.Clamp01(Mathf.Abs(Rigidbody.velocity.x / 3f));
        }

        float GetLiftByHeight()
        {
            return Mathf.Clamp(maximumHeight - Rigidbody.position.y, 0, 1);
        }

        float GetCountersteeringCoefficient(Vector2 input)
        {
            var countersteeringFactor = 1f;
            if (Mathf.Approximately(input.x, 0f))
            {
                return countersteeringFactor;
            }

            if (Math.SignOf(input.x) != Math.SignOf(Rigidbody.velocity.x))
            {
                countersteeringFactor = 3f;
            }

            return countersteeringFactor;
        }
    }
}
