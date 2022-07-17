using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    InputManager inputManager;
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed;

    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        #region Movemenet

        Vector2 inputMovement = inputManager.GetPlayerMovement();
        Vector3 move = transform.right * inputMovement.x + transform.forward * inputMovement.y;
        characterController.Move(move * speed * Time.deltaTime);
        #endregion
    }
}
