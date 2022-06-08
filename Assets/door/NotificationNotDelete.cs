using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace door
{
    public class NotificationNotDelete : MonoBehaviour
    {
        [SerializeField] private AudioClip clip;
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
            if (other.CompareTag("Player"))
            {
                AudioSource.PlayClipAtPoint(clip, other.transform.position, 2f);
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
        }
    }
}
