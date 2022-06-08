using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entermaze : MonoBehaviour
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
        }
    }
}
