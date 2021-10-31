using UnityEngine;
using Zenject;

namespace City.Crow
{
    public class LandsOnTriggerStay : MonoBehaviour
    {
        [Inject] CrowController Crow { get; }

        void OnTriggerStay2D(Collider2D other) => Crow.HasLanded = true;
    }
}
