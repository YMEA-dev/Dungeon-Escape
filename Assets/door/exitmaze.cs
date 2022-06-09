using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitmaze : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioObject;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            audioObject.Stop();
        }
    }
}
