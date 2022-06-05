using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NotificationSc")]
public class NotificationScriptable : ScriptableObject
{
    [Header("Message Customisation")] [SerializeField]
    internal Sprite yourIcon;

    [SerializeField] [TextArea] internal string notificationMessage;

    [Header("Notification Removal")] 
    [SerializeField]
    internal bool removeAfterExit = false;
    [SerializeField] internal bool disableAfterTimer = false;
    [SerializeField] internal float disableTimer = 1.0f;
}
