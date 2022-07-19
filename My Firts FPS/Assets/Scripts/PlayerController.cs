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

    [SerializeField] float gravityValue, jumpSpeed , groundDistance = 0.4f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;

    private void Start()
    {
        velocity.y += gravityValue * Time.deltaTime;

        inputManager = InputManager.Instance;
        vCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = mouseSens;
        vCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = mouseSens;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position , groundDistance , groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0;
        }
        
        velocity.y += gravityValue * Time.deltaTime;

        float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

        Vector2 inputMovement = inputManager.GetPlayerMovement();
        Vector3 move = transform.right * inputMovement.x + transform.forward * inputMovement.y;
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        playerBody.rotation = Quaternion.Euler(0f, vCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value, 0f);

        characterController.Move(move * speed * Time.deltaTime);
        characterController.Move(velocity * Time.deltaTime);
    }
}
