using UnityEngine;

namespace door
{
    public class TextZonePNJ : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
            
            }
        }
    }
}