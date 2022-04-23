using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollisionsManager : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        string colliderLayer = LayerMask.LayerToName(collision.gameObject.layer);
        if (colliderLayer == "Player")
            return;
        if (colliderLayer == "Ground")
            Debug.Log("Sword hit a wall");
    }
}
