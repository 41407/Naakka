using UnityEngine;
using Zenject;

namespace World.Crow
{
    public class CollisionBreaksFlight : MonoBehaviour
    {
        [Inject] CrowController Crow { get; }

        // ReSharper disable once UnusedParameter.Local
        void OnCollisionEnter2D(Collision2D other) => Crow.Crash();
    }
}
