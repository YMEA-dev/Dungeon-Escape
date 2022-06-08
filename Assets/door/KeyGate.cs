using UnityEngine;

namespace door
{
    public class KeyGate : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player") && GameVariables.keyCount > 1)
            {
                AudioSource.PlayClipAtPoint(clip, collider.transform.position, 2f);
                GameVariables.keyCount = 0;
                Destroy(gameObject);
            }
        }
    }
}
