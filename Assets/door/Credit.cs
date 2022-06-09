using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public GameObject player; //The object with the "playermovement"
    public ThirdPersonMovement script;//The playermovement script
    public GameObject gameOver; //The object "Game Over" of the Canvas
 
    void Start ()
    {
        gameOver.SetActive(false);   
        script = player.GetComponent<ThirdPersonMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            script.enabled = false;
            gameOver.SetActive(true);
        }
    }   
}
