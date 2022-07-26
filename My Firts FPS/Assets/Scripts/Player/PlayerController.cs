using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    [Header("Movement")]
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed, mouseSens;
    Vector3 direction;

    [Header("Camera Movement")]
    [SerializeField] Transform cameraTransform, playerBody;
    [SerializeField] CinemachineVirtualCamera vCamera;

    [Header("Physics")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float gravityValue, jumpHeigh, groundDistance, mass;
    [SerializeField] LayerMask groundMask;
    bool isGrounded, canMove;
    Vector3 weight, move, verticalVelocity;

    [Header("CheckCollisions")]
    [SerializeField] float rayMaxDistance;
    [SerializeField] float rayOffset;

    private void Start()
    {
        inputManager = InputManager.Instance;
        vCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = mouseSens;
        vCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = mouseSens;
    }

    private void Update()
    {
        #region Movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

        Vector2 inputMovement = inputManager.GetPlayerMovement();

        move = transform.right * inputMovement.x + transform.forward * inputMovement.y;
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;
        CalculateWeight();

        playerBody.rotation = Quaternion.Euler(0f, vCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value, 0f);

        characterController.Move(move * speed * Time.deltaTime);
        characterController.Move(verticalVelocity * Time.deltaTime);
        #endregion       
    }

    public void CalculateWeight()
    {
        if (isGrounded)
        {
            weight = Vector3.zero;
            if (inputManager.IsPlayerJumping())
            {
                verticalVelocity.y = Mathf.Sqrt(jumpHeigh * -2f * gravityValue);
            }
        }
        else
        {
            verticalVelocity += weight * Time.deltaTime;
            weight = new Vector3(0, mass * gravityValue, 0);
        }

        move += weight * Time.deltaTime;
    }
}
