using UnityEngine;

namespace door
{
    public class KeyGate : MonoBehaviour
    {
        [SerializeField] private AudioSource audioObject;
        private void Start()
                            {
                                audioObject.Stop();
                            } 
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player") && GameVariables.keyCount > 1)
            {
                audioObject.Play();
                GameVariables.keyCount = 0;
                Destroy(gameObject);
            }
        }
    }
}
