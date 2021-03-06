using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;
using Random = UnityEngine.Random;

public class ThirdPersonMovement : MonoBehaviour
{
    //public static ThirdPersonMovement Instance;
    
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask wallMask;
    
    public float gravity;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private float timeMidAir;

    public bool isGrounded;
    private bool jumpTrigger;
    
    private Vector3 velocity;

    private PhotonView PV;
    public CharacterStats myStats;
    [HideInInspector] public float health;
    [HideInInspector] public bool isSprinting;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        //Instance = this;
    }


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<CinemachineFreeLook>().gameObject);
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }

        health = myStats.BaseHealth;
    }

    /*public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        Debug.Log(gameObject);
        info.Sender.TagObject = gameObject;
    }*/
    
    void Update()
    {
        //Debug.Log(PhotonNetwork.OfflineMode);

        if (!PV.IsMine || PauseMenu.GameIsPaused)
        {
            return;
        }
        
        //Debug.Log("Health: " + health);

        Jump();
        Move();

        if (health <= 0)
            myStats.Die(gameObject);
    }

    private void Move()
    {
        if (!isGrounded)
            return;
        
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        
        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * myStats.Speed * Time.deltaTime *
                            (isSprinting ? myStats.SprintCoeff : 1));
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask | wallMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (jumpTrigger && isGrounded)
        {
            velocity.y = Mathf.Sqrt(-2f * myStats.JumpHeight * gravity);
            timeMidAir = 0;
            jumpTrigger = false;
        }

        if (!isGrounded)
        {
            timeMidAir += Time.deltaTime;
            controller.Move(controller.transform.forward
                * Time.deltaTime * myStats.Speed / (timeMidAir + 1));
        }
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void EnableJumpTrigger()
    {
        jumpTrigger = true;
    }
}