using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWeaponCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("COLLISION");
        string colliderLayer = LayerMask.LayerToName(collision.gameObject.layer);
        if (colliderLayer == "Player")
            return;
        
        if (colliderLayer == "Ground")
            Debug.Log("Sword hit a wall");
        if (colliderLayer == "Enemy")
            Debug.Log("Sword hit an enemy");
    }
}
