using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed, mouseSens;
    [SerializeField] Transform cameraTransform , playerBody;
    [SerializeField] CinemachineVirtualCamera vCamera;

    private void Start()
    {
        inputManager = InputManager.Instance;
        vCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = mouseSens;
        vCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = mouseSens;
    }

    private void Update()
    {
        float mouseHorizontal = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;

        Vector2 inputMovement = inputManager.GetPlayerMovement();
        Vector3 move = transform.right * inputMovement.x + transform.forward * inputMovement.y;
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;

        playerBody.rotation = Quaternion.Euler(0f, vCamera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.Value, 0f); 

        characterController.Move(move * speed * Time.deltaTime);
    }
}
