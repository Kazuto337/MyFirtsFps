using UnityEngine;
using Cinemachine;

public class CinemachinePOV_Extension : CinemachineExtension
{
    public float mouseSens = 20f, clampAngle = 80f;
    InputManager inputManager;
    private Vector3 staringRotation;
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        base.Awake();
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (staringRotation == null) staringRotation = transform.localRotation.eulerAngles;
                Vector2 deltaInput = inputManager.GetMouseMovement();
                staringRotation.x += deltaInput.x * Time.deltaTime * mouseSens;
                staringRotation.y += deltaInput.y * Time.deltaTime * mouseSens;
                staringRotation.y = Mathf.Clamp(staringRotation.y, -clampAngle, clampAngle);

                state.RawOrientation = Quaternion.Euler(-staringRotation.y , -staringRotation.x , 0f);
            }
        }
    }
}
