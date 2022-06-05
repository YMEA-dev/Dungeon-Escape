using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGate : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "player")
        {
            Destroy(gameObject);
        }
    }
}
