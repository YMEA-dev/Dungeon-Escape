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
        else if (colliderLayer == "Enemy")
            Debug.Log("Sword hit an enemy");
        else if (colliderLayer == "Wall")
        {
            CombatAnimationStateController.Instance.stunActivated = true;
            Debug.Log("Weapon hit a wall");
        }
    }
}
