using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed;
    Transform cameraTransform;

    private void Start()
    {
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Vector2 inputMovement = inputManager.GetPlayerMovement();
        Vector3 move = transform.right * inputMovement.x + transform.forward * inputMovement.y;
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0f;
        characterController.Move(move * speed * Time.deltaTime);
    }
}
