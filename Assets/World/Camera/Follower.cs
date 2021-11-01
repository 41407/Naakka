using UnityEngine;

namespace World.Camera
{
    public class Follower : MonoBehaviour
    {
        [SerializeField] Transform target;

        Transform Target => target;
        Vector3 Offset { get; set; }

        Vector3 TargetPosition => Target.position + Offset;

        void Awake()
        {
            Offset = transform.position - Target.position;
        }

        void LateUpdate() => LerpTowards(TargetPosition);

        void LerpTowards(Vector3 targetPosition)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.25f);
        }
    }
}
