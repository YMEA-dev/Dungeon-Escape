using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
