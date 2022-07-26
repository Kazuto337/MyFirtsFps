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

    [Header("Camera Movement")]
    [SerializeField] Transform cameraTransform, playerBody;
    [SerializeField] CinemachineVirtualCamera vCamera;

    [SerializeField] float gravityValue, jumpHeigh, groundDistance, mass;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    Vector3 weight, move, verticalVelocity;

    private void Start()
    {
        inputManager = InputManager.Instance;
        vCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = mouseSens;
        vCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = mouseSens;
    }

    private void Update()
    {
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
