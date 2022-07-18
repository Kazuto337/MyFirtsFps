using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    static InputManager _instance;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    PlayerControls playerControls;
    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else _instance = this;

        playerControls = new PlayerControls();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnEnable()
    {
        playerControls.Enable();
    }

    public void OnDisable()
    {
        playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return playerControls.Player.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetMouseMovement()
    {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }

    public bool IsPlayerJumping()
    {
        return playerControls.Player.Jump.triggered;
    }
}
