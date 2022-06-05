using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyitem : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.name == "player")
        {
            GameVariables.keyCount++;
            Destroy(gameObject);
        }
    }
}
