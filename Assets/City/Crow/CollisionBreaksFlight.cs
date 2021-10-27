using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class CollisionBreaksFlight : MonoBehaviour
    {
        [Inject] CrowController Crow { get; }

        void OnCollisionEnter2D(Collision2D other) => Crow.Crash();
    }
}
