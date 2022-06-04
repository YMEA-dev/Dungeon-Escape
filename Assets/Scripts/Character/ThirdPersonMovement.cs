using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Photon.Pun;

public class ThirdPersonMovement : MonoBehaviour
{
    public static ThirdPersonMovement Instance;
    
    public CharacterController controller;
    public Transform cam;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public LayerMask wallMask;
    
    public float gravity = -9.81f;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;

    public bool isGrounded;
    
    private Vector3 velocity;

    private PhotonView PV;
    public CharacterStats myStats;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        Instance = this;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<CinemachineFreeLook>().gameObject);
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }

        myStats.Health = myStats.BaseHealth;
    }
    
    void Update()
    {
        if (!PV.IsMine)
            return;

        Jump();
        Move();
        
        if (myStats.Health <= 0)
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
            controller.Move(moveDir.normalized * myStats.Speed * Time.deltaTime);
        }
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask | wallMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;
        
        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(-2f * myStats.JumpHeight * gravity);    
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
