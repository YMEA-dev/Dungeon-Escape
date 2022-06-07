using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
