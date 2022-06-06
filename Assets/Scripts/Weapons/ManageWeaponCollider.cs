using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ManageWeaponCollider : MonoBehaviour
{
    public Transform pivotWeaponRight;
    
    private BoxCollider weaponCollider;
    private GameObject swordHolder;

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        swordHolder = pivotWeaponRight.GetChild(0).gameObject;
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
