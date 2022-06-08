using UnityEngine;

namespace door
{
    public class KeyGate : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player") && GameVariables.keyCount > 1)
            {
                GameVariables.keyCount = 0;
                Destroy(gameObject);
            }
        }
    }
}
