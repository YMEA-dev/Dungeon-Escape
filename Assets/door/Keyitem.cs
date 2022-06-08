using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyitem : MonoBehaviour
{
    
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameVariables.keyCount++;
            
            Destroy(gameObject);
            
        }
    }
}
