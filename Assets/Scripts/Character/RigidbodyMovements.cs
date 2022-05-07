using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Photon.Pun;
using Photon.Realtime;
using Unity.Mathematics;

public class RigidbodyMovements : MonoBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    private float xRot;
    private float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    [SerializeField] private LayerMask Ground;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private Rigidbody PlayerBody;
    [SerializeField] private Transform PlayerCamera;

    [SerializeField] private float Speed;
    [SerializeField] private float Sensitivity;
    [SerializeField] private float JumpForce;

    private PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<CinemachineFreeLook>().gameObject);
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    void Update()
    {
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 moveVector = PlayerCamera.transform.rotation * PlayerMovementInput * Speed;
        PlayerBody.AddForce(moveVector.x, 0, moveVector.z);
        if (moveVector.magnitude >= 0.1f)
            transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);

        if (Input.GetKeyDown(KeyCode.Space))
            if (Physics.CheckSphere(GroundCheck.position, 0.1f, Ground)) 
                PlayerBody.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }

    void MovePlayerCamera()
    {
        /*xRot -= PlayerMouseInput.y * Sensitivity;

        transform.Rotate(0f, PlayerMouseInput.x * Sensitivity, 0f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);*/
    }
}
