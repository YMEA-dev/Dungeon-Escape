using UnityEngine;

namespace door
{
    public class NormalGate : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
