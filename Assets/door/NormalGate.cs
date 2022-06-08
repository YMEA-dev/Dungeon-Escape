using UnityEngine;

namespace door
{
    public class NormalGate : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(clip, collider.transform.position, 2f);
                Destroy(gameObject);
            }
        }
    }
}
