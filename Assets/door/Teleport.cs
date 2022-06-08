using Photon.Realtime;
using UnityEngine;

namespace door
{
    public class Teleport : MonoBehaviour
    {
        public GameObject tppoint;
    
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log(other.transform.position);
                other.transform.position = tppoint.transform.position;
                Debug.Log(other.transform.position);
            }
        }
    }
}
