using UnityEngine;

namespace door
{
    public class NormalGate : MonoBehaviour
    {
        [SerializeField] private AudioSource audioObject;
        private void Start()
        {
            audioObject.Stop();
        } 
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player"))
            {
               audioObject.Play();
                Destroy(gameObject);
            }
        }
    }
}
