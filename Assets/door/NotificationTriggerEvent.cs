using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NotificationTriggerEvent : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private Transform TeleportTo;
    [SerializeField] private Text notificationTextUI;
    [SerializeField] private Image itemIconUI;
    [SerializeField] private Sprite yourIcon;
    [SerializeField] [TextArea] private string notificationMessage;
    [SerializeField] private Animator notificationAnim;
    [SerializeField] private bool removeAfterExit = false;
    [SerializeField] private bool disableAfterTime = false;
    [SerializeField]  float DisableTime = 1.0f;
    private BoxCollider objectCollider;

    private void Awake()
    {
        objectCollider = gameObject.GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Player.transform.position = TeleportTo.transform.position;  
        if (other.CompareTag("Player"))
        {
            
            Debug.Log("take key");
            StartCoroutine(EnableNotification());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && removeAfterExit)
        {
            RemoveNotification();
        }
    }
    
        IEnumerator EnableNotification()
        {
            objectCollider.enabled = false;
            notificationAnim.Play("NotificationFadeIn");
            notificationTextUI.text = notificationMessage;
            itemIconUI.sprite = yourIcon;
            if (disableAfterTime)
            {
                yield return new WaitForSeconds(DisableTime);
                RemoveNotification();
            }
        }

        void RemoveNotification()
        {
            notificationAnim.Play("NotificationFadeOut");
            GameVariables.keyCount++;
            gameObject.SetActive(false);
        }
}

