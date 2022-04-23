using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeaponCollider : MonoBehaviour
{
    public Transform pivotWeaponRight;
    
    private BoxCollider weaponCollider;
    private GameObject swordHolder;

    private void Start()
    {
        swordHolder = pivotWeaponRight.GetChild(3).gameObject;
        weaponCollider = swordHolder.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;
    }

    public void AttackStart()
    {
        weaponCollider.enabled = true;
        //Debug.Log("Starting Attack " + weaponCollider.enabled);
    }

    public void AttackEnd()
    {
        weaponCollider.enabled = false;
        //Debug.Log("Ending Attack " + weaponCollider.enabled);
    }
}
